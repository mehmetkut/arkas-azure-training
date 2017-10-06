using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoApp.Controllers
{
    public class SignupController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> Index(string email)
        {
            await JobHelper.SendNotification(email);
            return RedirectToAction("index", "home");
        }
    }
}
