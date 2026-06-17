# 🍽️ Sahara Restoran — SignalR Tabanlı Restoran Yönetim Sistemi

Gerçek zamanlı bir **restoran sipariş ve yönetim platformu**. Müşterilerin masa bazlı sipariş verebildiği halka açık bir web sitesi ile, yöneticilerin tüm işletmeyi canlı istatistiklerle yönettiği bir admin panelini tek bir çözümde birleştirir.

Sistem; **katmanlı mimari (N-Tier)**, **CQRS (MediatR)**, **SignalR ile gerçek zamanlı iletişim** ve **API tüketen MVC arayüzü** üzerine kuruludur.

![.NET](https://img.shields.io/badge/.NET-6.0-512BD4?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-6.0-512BD4)
![SignalR](https://img.shields.io/badge/SignalR-RealTime-FF6C37)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green)

---

## 📋 İçindekiler
- [Genel Bakış](#-genel-bakış)
- [Mimari](#-mimari)
- [Özellikler](#-özellikler)
- [Kullanılan Tasarım Desenleri (Patterns)](#-kullanılan-tasarım-desenleri-patterns)
- [Gerçek Zamanlı Akış (SignalR)](#-gerçek-zamanlı-akış-signalr)
- [Proje Yapısı](#-proje-yapısı)
- [Teknoloji Yığını](#-teknoloji-yığını)

---

## 🎯 Genel Bakış

Proje iki çalıştırılabilir uygulamadan oluşur:

| Uygulama | Rol |
|----------|-----|
| **SignalRApi** | RESTful Web API + SignalR Hub. Tüm iş kuralları ve veri erişimi buradan sunulur. |
| **SignalRWebUI** | ASP.NET Core MVC. Hem halka açık restoran sitesi hem de yönetici paneli. API'yi `HttpClient` üzerinden tüketir. |

Bu iki uygulama arasındaki tüm veri alışverişi HTTP/JSON ile, gerçek zamanlı bildirimler ise WebSocket (SignalR) ile sağlanır.

---

## 🏗️ Mimari

Proje **soyutlama temelli, katmanlı (N-Tier) bir mimari** izler. Her katman yalnızca bir alttakine bağımlıdır ve katmanlar arası iletişim arayüzler (interface) üzerinden gerçekleşir.

```
┌──────────────────────────────────────────────────────────────┐
│  Presentation                                                  │
│  ┌────────────────────┐        ┌────────────────────────────┐ │
│  │   SignalRWebUI      │  HTTP  │        SignalRApi          │ │
│  │  (MVC + Admin)      │ ─────► │  (Web API + SignalR Hub)   │ │
│  │  ViewComponents     │ ◄────  │  AutoMapper / Validation   │ │
│  └────────────────────┘  WS    └─────────────┬──────────────┘ │
└──────────────────────────────────────────────│────────────────┘
                                                ▼
                          ┌──────────────────────────────────┐
                          │      SignalR.BusinessLayer        │
                          │  Manager'lar + MediatR (CQRS)     │
                          └─────────────────┬─────────────────┘
                                            ▼
                          ┌──────────────────────────────────┐
                          │     SignalR.DataAccessLayer       │
                          │  Generic Repository + EF Core     │
                          └─────────────────┬─────────────────┘
                                            ▼
                          ┌──────────────────────────────────┐
                          │   SignalR.EntityLayer (POCO)      │
                          │   SignalR.DtoLayer  (DTO'lar)     │
                          └──────────────────────────────────┘
```

**Bağımlılık yönü tamamen aşağıya doğrudur** — sunum katmanı iş kurallarını, iş katmanı da veri erişimini yalnızca arayüzler üzerinden tanır. Somut sınıflar `Program.cs` içinde **Dependency Injection** ile bağlanır.

---

## ✨ Özellikler

### 🌐 Halka Açık Site (Müşteri)
- **Ana sayfa**: dinamik slider, kampanya/indirim kartları, "menümüz" bölümü, hakkımızda, rezervasyon çağrısı, müşteri yorumları ve iletişim formu — tamamı veritabanından beslenir.
- **Menü**: ürünler kategoriye göre **dinamik filtreleme** ile listelenir.
- **Masa bazlı sepet sistemi**: müşteri masa numarasıyla ürünleri sepete ekler; sepet ürünleri ve toplam tutar (KDV dahil) canlı hesaplanır.
- **Sipariş tamamlama**: sepet onaylandığında `Order` + `OrderDetail` kayıtları oluşturulur, sepet temizlenir ve masa durumu otomatik **boş**a alınır.
- **Rezervasyon**: form ile masa rezervasyonu (FluentValidation ile doğrulanır).
- **Leziz Tarifler**: harici **RapidAPI (Tasty)** servisinden yemek tarifleri çekilir; kota/timeout hataları zarifçe yönetilir.
- **İletişim & Konum**: footer'da tıklanabilir Google Maps konumu, telefon (`tel:`) ve e-posta (`mailto:`) bağlantıları.

### 🔐 Yönetici Paneli (Auth Gerekli)
- **Canlı Dashboard**: kategori/ürün/sipariş sayıları, ciro, en yüksek/düşük fiyatlı ürün, kasa toplamı gibi KPI'lar **SignalR ile anlık** güncellenir.
- **CRUD Yönetimi**: Kategori, Ürün, Rezervasyon, İndirim, Slider, Müşteri Yorumu, Sosyal Medya, Hakkımızda, İletişim, Masa ve Bildirim modülleri.
- **Ek araçlar**: e-posta gönderimi, **QR kod oluşturma/çözümleme**, performans grafikleri, profil ayarları.
- **Kimlik doğrulama**: ASP.NET Core Identity ile giriş/çıkış; tüm panel global yetkilendirme filtresiyle korunur.

---

## 🧩 Kullanılan Tasarım Desenleri (Patterns)

| Desen | Açıklama ve Projedeki Karşılığı |
|-------|--------------------------------|
| **N-Tier Layered Architecture** | Entity / DTO / DataAccess / Business / Presentation katmanları net biçimde ayrılmıştır. |
| **Repository Pattern + Generic Repository** | `IGenericDal<T>` ve `GenericRepository<T>` ile ortak CRUD; her varlık için `EfXDal` özelleşmesi. Tekrarlayan veri erişim kodu tek yerde toplanır. |
| **Service / Manager Layer** | `IGenericService<T>` ve `XManager` sınıfları iş kurallarını veri erişiminden soyutlar. |
| **DTO Pattern + AutoMapper** | Entity'ler API yüzeyine doğrudan açılmaz; her varlık için `Profile` sınıfları (`CreateMap`, `ReverseMap`) tanımlıdır. İç içe navigasyon alanları (ör. `Category.CategoryName`) için açık `ForMember` eşlemeleri kullanılır. |
| **CQRS (MediatR)** | Dashboard istatistikleri okuma tarafında **Query + Handler** ayrımıyla yönetilir (`GetProductCountQuery`, `TodayTotalPriceQuery` vb.). Hub yalnızca `IMediator.Send` çağırır; iş mantığı handler'lardadır. |
| **Dependency Injection** | Tüm servis/repository/validator bağımlılıkları `Program.cs`'te `AddScoped` ile kaydedilir. |
| **FluentValidation** | DTO doğrulama kuralları (`CreateBookingDtoValidator`) ayrı sınıflarda; validator'lar açık biçimde (`AddScoped<IValidator<T>, ...>`) kaydedilir. |
| **ViewComponent Pattern** | Navbar, footer, slider, kampanyalar, menü, kategori filtresi, KPI kartları gibi tekrar eden UI parçaları bağımsız, yeniden kullanılabilir bileşenlere bölünmüştür. |
| **API Gateway / HttpClient Tüketimi** | MVC arayüzü veriye doğrudan DB'den değil, `IHttpClientFactory` ile API'den erişir — sunum ve veri katmanı tamamen ayrıktır. |
| **Global Authorization + AllowAnonymous** | Panel varsayılan olarak kilitlidir; halka açık sayfalar `[AllowAnonymous]` ile beyaz listeye alınır. |

---

## ⚡ Gerçek Zamanlı Akış (SignalR)

`SignalRHub`, MediatR ile birleşerek paneli canlı tutar:

- **`SendStatistic`** → 16 farklı KPI'ı (sayımlar, ortalama/uç fiyatlar, ciro, kasa) anlık yayınlar.
- **`SendNotification`** → okunmamış bildirim sayısı ve listesi.
- **`GetOrderTableList` / `GetBookingList`** → masa durumları ve rezervasyonlar tüm istemcilere (`Clients.All`) anlık iletilir.
- **`SendMessage`** → basit gerçek zamanlı mesajlaşma.
- **Online istemci sayacı** → `OnConnectedAsync` / `OnDisconnectedAsync` ile bağlı kullanıcı sayısı canlı güncellenir.

Bu sayede bir masa sipariş aldığında veya boşaldığında, panel **sayfa yenilemeden** anında güncellenir.

---

## 📁 Proje Yapısı

```
SignalRProject/
├── SignalR.EntityLayer/          # POCO varlıklar (Product, Order, Basket, Category, AppUser...)
├── SignalR.DtoLayer/             # Katmanlar arası veri taşıyıcılar (Result/Create/Update/GetById DTO'ları)
├── SignalR.DataAccessLayer/
│   ├── Abstract/                 # IGenericDal<T>, IXDal arayüzleri
│   ├── Concrete/                 # SignalRContext (EF Core DbContext), Identity sınıfları
│   └── EntityFramework/          # GenericRepository<T>, EfXDal somut repository'leri
├── SignalR.BusinessLayer/
│   ├── Abstract/                 # IGenericService<T>, IXService arayüzleri
│   ├── Concrete/                 # XManager iş kuralları
│   └── MediatR/Queries/          # CQRS Query + Handler (dashboard istatistikleri)
├── SignalRApi/
│   ├── Controllers/              # RESTful API uç noktaları
│   ├── Mapping/                  # AutoMapper Profile'ları
│   ├── Validators/               # FluentValidation kuralları
│   └── Hubs/SignalRHub.cs        # Gerçek zamanlı hub
└── SignalRWebUI/
    ├── Controllers/              # Halka açık + admin MVC controller'ları
    ├── ViewComponents/           # Modüler UI bileşenleri
    ├── Views/                    # Razor görünümleri (Feane teması + Tailwind admin)
    └── wwwroot/                  # Statik dosyalar, tema, görseller
```

---

## 🛠️ Teknoloji Yığını

**Backend**
- ASP.NET Core 6.0 (Web API + MVC)
- Entity Framework Core 6 (Code-First, SQL Server)
- AutoMapper 12 · MediatR 11 · FluentValidation
- ASP.NET Core SignalR
- ASP.NET Core Identity

**Frontend**
- Razor Views + ViewComponents
- Stitch Aı tasarım promptları
- Bootstrap 4 / Feane teması (halka açık site)
- Tailwind CSS (yönetici sidebar)
- jQuery + AJAX, SweetAlert

**Entegrasyonlar**
- RapidAPI — Tasty (yemek tarifleri)
- QR Code üretimi/çözümlemesi
- SMTP ile e-posta gönderimi

---

![1](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010250.png)
![2](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010313.png)
![3](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010327.png)
![4](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010337.png)
![5](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010349.png)
![6](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010356.png)
![7](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010418.png)
![8](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010435.png)
![9](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010512.png)
![10](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010452.png)
![11](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010442.png)
![12](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010525.png)
![13](https://raw.githubusercontent.com/ZiyaBurakYayla/SignalRProject/refs/heads/Default/SignalRWebUI/Images/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202026-06-18%20010551.png)

