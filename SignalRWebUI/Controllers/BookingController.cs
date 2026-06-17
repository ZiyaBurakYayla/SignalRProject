using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BookingDtos;

namespace SignalRWebUI.Controllers
{
    public class BookingController : Controller
    {
        private readonly IHttpClientFactory _ClientFactory;

        public BookingController(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }

        public async Task<IActionResult> BookingList()
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7196/api/Bookings");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBookingDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateBooking()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
        {
            createBookingDto.Description = "Rezervasyon Onaylandı";
            var client = _ClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBookingDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            await client.PostAsync("https://localhost:7196/api/Bookings", content);
            return RedirectToAction("BookingList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBooking(int id)
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7196/api/Bookings/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateBookingDto>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            var client = _ClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBookingDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7196/api/Bookings", content);
            return RedirectToAction("BookingList");
        }

        public async Task<IActionResult> DeleteBooking(int id)
        {
            var client = _ClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7196/api/Bookings/{id}");
            return RedirectToAction("BookingList");
        }

        public async Task<IActionResult> BookingStatusApproved(int id)
        {
            var client = _ClientFactory.CreateClient();
            await client.GetAsync($"https://localhost:7196/api/Bookings/BookingStatusApproved?id={id}");
            return RedirectToAction("BookingList");
        }

        public async Task<IActionResult> BookingStatusCancelled(int id)
        {
            var client = _ClientFactory.CreateClient();
            await client.GetAsync($"https://localhost:7196/api/Bookings/BookingStatusCancelled?id={id}");
            return RedirectToAction("BookingList");
        }
    }
}
