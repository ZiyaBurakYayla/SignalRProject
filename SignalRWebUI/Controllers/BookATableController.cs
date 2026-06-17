using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SignalRWebUI.Dtos.BookingDtos;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class BookATableController : Controller
    {
        private readonly IHttpClientFactory _ClientFactory;

        public BookATableController(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await SetLocationAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateBookingDto createBookingDto)
        {
            await SetLocationAsync();
            createBookingDto.Description = "Rezervasyon Beklemede";
            ModelState.Remove("Description");
            var client = _ClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7196/api/Bookings", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errorMessage = await responseMessage.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, errorMessage);
                return View();
            }
                
        }

        private async Task SetLocationAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7196/api/Contacts");
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                var item = JArray.Parse(body);
                if (item.Count > 0)
                    ViewBag.location = item[0]["location"]?.ToString();
            }
        }
    }
}
