using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BasketDtos;
using SignalRWebUI.Dtos.ProductDtos;

namespace SignalRWebUI.Controllers
{
    [AllowAnonymous]
    public class MenuController : Controller
    {

        private readonly IHttpClientFactory _ClientFactory;

        public MenuController(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.v = id;

            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7196/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View(); ;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(int id,int OrderTableId)
        {
            if(OrderTableId == 0)
            {
                return BadRequest("Table değeri 0 geliyor");
            }
            CreateBasketDto createBasketDto = new CreateBasketDto();
            createBasketDto.ProductId = id;
            createBasketDto.OrderTableId = OrderTableId;
            var client = _ClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBasketDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7196/api/Baskets", content);

            var client2 = _ClientFactory.CreateClient();
            await client2.GetAsync("https://localhost:7196/api/OrderTables/ChangeOrderTableStatusToTrue/"+OrderTableId);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return Json(createBasketDto);
        }
    }
}
