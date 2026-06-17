using Microsoft.AspNetCore.Mvc;

namespace SignalRWebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
