using Microsoft.AspNetCore.Mvc;

namespace Foody.WebUI.ViewComponents.MenuDefaultComponents
{
    public class _MenuDefaultComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
