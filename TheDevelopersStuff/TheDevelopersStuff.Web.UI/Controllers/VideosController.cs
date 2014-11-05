using System.Web.Mvc;

namespace TheDevelopersStuff.Web.UI.Controllers
{
    public class VideosController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", null, "It Works");
        }
    }
}