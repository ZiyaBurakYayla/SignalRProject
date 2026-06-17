using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalR.WebUI.Dtos.OrderTableDtos;

namespace SignalRWebUI.Controllers
{
    public class OrderTableController : Controller
    {
        private readonly IHttpClientFactory _ClientFactory;

        public OrderTableController(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }

        public async Task<IActionResult> OrderTableList()
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7196/api/OrderTables");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOrderTableDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateOrderTable()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderTable(CreateOrderTableDto createOrderTableDto)
        {
            var client = _ClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createOrderTableDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7196/api/OrderTables", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("OrderTableList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOrderTable(int id)
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7196/api/OrderTables/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateOrderTableDto>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderTable(UpdateOrderTableDto updateOrderTableDto)
        {
            var client = _ClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateOrderTableDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7196/api/OrderTables", content);
            return RedirectToAction("OrderTableList");
        }

        public async Task<IActionResult> DeleteOrderTable(int id)
        {
            var client = _ClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7196/api/OrderTables/{id}");
            return RedirectToAction("OrderTableList");
        }
    }
}
