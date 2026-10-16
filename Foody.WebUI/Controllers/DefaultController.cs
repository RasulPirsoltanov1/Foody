using Microsoft.AspNetCore.Mvc;

namespace Foody.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
