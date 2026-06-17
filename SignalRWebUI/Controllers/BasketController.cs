using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalR.EntityLayer.Entities;
using SignalRWebUI.Dtos.BasketDtos;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class BasketController : Controller
    {
        private readonly IHttpClientFactory _ClientFactory;

        public BasketController(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }

        public async Task<IActionResult> Index(int id)
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7196/api/Baskets/BasketsListByMenuTableWithProductName?tableNumber={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBasketDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> CompleteOrder(int orderTableId)
        {
            var client = _ClientFactory.CreateClient();
            await client.GetAsync($"https://localhost:7196/api/Baskets/CompleteOrder/{orderTableId}");
            return RedirectToAction("Index", "Default");
        }

        public async Task<IActionResult> DeleteBasket(int id, int orderTableId)
        {
            var client = _ClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7196/api/Baskets/{id}");

            var client2 = _ClientFactory.CreateClient();
            var remaining = await client2.GetAsync($"https://localhost:7196/api/Baskets/GetBasketByOrderTableNumber?tableNumber={orderTableId}");
            if (!remaining.IsSuccessStatusCode)
            {
                var client3 = _ClientFactory.CreateClient();
                await client3.GetAsync($"https://localhost:7196/api/OrderTables/ChangeOrderTableStatusToFalse/{orderTableId}");
            }

            return RedirectToAction("Index", new { id = orderTableId });
        }

    }
}
