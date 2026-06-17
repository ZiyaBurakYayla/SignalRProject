using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.ContactDtos;

namespace SignalRWebUI.ViewComponents.UILayoutComponents
{
    public class UILayoutFooterComponent : ViewComponent
    {
        private readonly IHttpClientFactory _ClientFactory;

        public UILayoutFooterComponent(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7196/api/Contacts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
                return View(values);
            }
            return View(); ;
        }
    }
}
