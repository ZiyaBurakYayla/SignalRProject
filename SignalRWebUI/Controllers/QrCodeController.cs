using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ZXing;
using ZXing.Common;

namespace SignalRWebUI.Controllers
{
    public class QrCodeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(string value, string size = "medium", string darkColor = "#000000", string lightColor = "#ffffff")
        {
            if (string.IsNullOrWhiteSpace(value)) return View();
            int px = size switch { "small" => 5, "large" => 20, "xlarge" => 33, _ => 10 };
            var darkRgba  = HexToRgba(darkColor);
            var lightRgba = HexToRgba(lightColor);
            using var data   = new QRCodeGenerator().CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(data);
            byte[] png = qrCode.GetGraphic(px, darkRgba, lightRgba);
            ViewBag.QrCodeImage = "data:image/png;base64," + Convert.ToBase64String(png);
            return View();
        }

        [HttpGet]
        public IActionResult Decode() => View();

        [HttpPost]
        public IActionResult Decode(IFormFile file)
        {
            if (file == null || file.Length == 0) { ViewBag.Error = "Lütfen bir görsel seçin."; return View(); }
            try
            {
                using var bitmap = new Bitmap(file.OpenReadStream());
                var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                var bytes = new byte[Math.Abs(data.Stride) * bitmap.Height];
                Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
                bitmap.UnlockBits(data);
                var src = new RGBLuminanceSource(bytes, bitmap.Width, bitmap.Height, RGBLuminanceSource.BitmapFormat.BGRA32);
                Result result;
                try   { result = new MultiFormatReader().decode(new BinaryBitmap(new HybridBinarizer(src))); }
                catch { result = null; }
                if (result != null) ViewBag.Result = result.Text;
                else ViewBag.Error = "QR kod okunamadı. Görseli kontrol edin.";
            }
            catch { ViewBag.Error = "Görsel işlenirken hata oluştu."; }
            return View();
        }

        private static byte[] HexToRgba(string hex)
        {
            hex = hex.TrimStart('#');
            return new byte[]
            {
                Convert.ToByte(hex[0..2], 16),
                Convert.ToByte(hex[2..4], 16),
                Convert.ToByte(hex[4..6], 16),
                255
            };
        }
    }
}
