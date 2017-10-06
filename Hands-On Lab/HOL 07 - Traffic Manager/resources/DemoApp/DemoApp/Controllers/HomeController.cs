using System.Web.Mvc;

namespace TrafficManagerDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                RegionName = ConfigService.GetRegionName()
            };

            return View(viewModel);
        }
    }
}