using System.Web.Mvc;

namespace TrafficManagerDemo.Web.Controllers
{
    public class PingController : Controller
    {
        public ActionResult Index()
        {
            return new ContentResult
            {
                ContentType = "text/plain",
                Content = ConfigService.GetRegionName()
            };
        }
    }
}