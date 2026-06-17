using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.AboutDtos;

namespace SignalRWebUI.ViewComponents.DefaultComponents
{
    public class DefaultAboutComponent : ViewComponent
    {
        private readonly IHttpClientFactory _ClientFactory;

        public DefaultAboutComponent(IHttpClientFactory clientFactory)
        {
            _ClientFactory = clientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _ClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7196/api/Abouts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(values);
            }
            return View(); ;
        }
    }
}
