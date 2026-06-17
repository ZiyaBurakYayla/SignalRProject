using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.RapidApiDtos;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class FoodRapidApiController : Controller
    {
        private readonly IConfiguration _configuration;

        public FoodRapidApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var apiKey = _configuration["RapidApi:TastyApiKey"];
            var apiHost = _configuration["RapidApi:TastyApiHost"];

            var client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://tasty.p.rapidapi.com/recipes/list?from=0&size=20"),
                Headers =
                {
                    { "x-rapidapi-key", apiKey },
                    { "x-rapidapi-host", apiHost },
                },
            };
            try
            {
                using var response = await client.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = $"Tarif servisi şu an kullanılamıyor ({(int)response.StatusCode}). Lütfen daha sonra tekrar deneyin.";
                    return View(new List<ResultTastyApiDto>());
                }
                var root = JsonConvert.DeserializeObject<RootTastyApi>(body);
                return View(root?.Results ?? new List<ResultTastyApiDto>());
            }
            catch (TaskCanceledException)
            {
                ViewBag.Error = "Tarif servisi zaman aşımına uğradı. Lütfen daha sonra tekrar deneyin.";
                return View(new List<ResultTastyApiDto>());
            }
        }
    }
}
