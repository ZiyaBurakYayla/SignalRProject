using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.ContactDtos;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class DefaultBookATableComponent : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public DefaultBookATableComponent(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _clientFactory.CreateClient();
            try
            {
                var response = await client.GetAsync("https://localhost:7196/api/Contacts");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var contacts = JsonConvert.DeserializeObject<List<ResultContactDto>>(json);
                    var location = contacts?.FirstOrDefault()?.Location;
                    ViewBag.Location = location;
                }
            }
            catch { }

            return View();
        }
    }
}
