using Microsoft.AspNetCore.Mvc;

namespace Foody.WebUI.ViewComponents
{
    public class _FeatureDefaultComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
  