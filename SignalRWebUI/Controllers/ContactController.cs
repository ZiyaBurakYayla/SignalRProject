using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.ContactDtos;

namespace SignalRWebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _ClientFactory;

        public ContactController(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }

        public async Task<IActionResult> ContactList()
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7196/api/Contacts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            var client = _ClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createContactDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            await client.PostAsync("https://localhost:7196/api/Contacts", content);
            return RedirectToAction("ContactList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id)
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7196/api/Contacts/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateContactDto>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            var client = _ClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateContactDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7196/api/Contacts", content);
            return RedirectToAction("ContactList");
        }

        public async Task<IActionResult> DeleteContact(int id)
        {
            var client = _ClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7196/api/Contacts/{id}");
            return RedirectToAction("ContactList");
        }
    }
}
