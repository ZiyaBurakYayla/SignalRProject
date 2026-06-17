using System.Text;

namespace SignalRWebUI.Helpers
{
    public static class CategoryHelper
    {
        // Kategori adını isotope filtresinde kullanılabilecek güvenli bir CSS class'ına çevirir.
        // Örn: "İçecekler" -> "icecekler", "Ana Yemek" -> "anayemek"
        public static string Slugify(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "kategori";

            name = name.Trim().ToLowerInvariant()
                .Replace("ç", "c")
                .Replace("ğ", "g")
                .Replace("ı", "i")
                .Replace("i̇", "i") // noktalı küçük i (İ'nin ToLower hali)
                .Replace("ö", "o")
                .Replace("ş", "s")
                .Replace("ü", "u");

            var sb = new StringBuilder();
            foreach (var ch in name)
            {
                if (ch >= 'a' && ch <= 'z' || ch >= '0' && ch <= '9')
                    sb.Append(ch);
            }

            var slug = sb.ToString();
            return string.IsNullOrEmpty(slug) ? "kategori" : slug;
        }
    }
}
