using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;

namespace SignalRWebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _ClientFactory;

        public ProductController(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }
        public async Task<IActionResult> ProductList()
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7196/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductByCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7196/api/Categories");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            List<SelectListItem> categoryList = (from c in categories
                                                 select new SelectListItem
                                                 {
                                                     Text = c.CategoryName,
                                                     Value = c.CategoryId.ToString()
                                                 }).ToList();

            ViewBag.categories = categoryList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client = _ClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7196/api/Products", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client1 = _ClientFactory.CreateClient();
            var responseMessage1 = await client1.GetAsync("https://localhost:7196/api/Categories");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData1);
            List<SelectListItem> categoryList = (from c in categories
                                                 select new SelectListItem
                                                 {
                                                     Text = c.CategoryName,
                                                     Value = c.CategoryId.ToString()
                                                 }).ToList();
            ViewBag.values = categoryList;

            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7196/api/Products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = _ClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7196/api/Products", content);
            return RedirectToAction("ProductList");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _ClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7196/api/Products/{id}");
            return RedirectToAction("ProductList");
        }
    }
}
