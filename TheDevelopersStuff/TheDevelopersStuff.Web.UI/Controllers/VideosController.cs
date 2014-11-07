using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Web.UI.Controllers
{
    public class VideosController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", null, "It Works");
        }

        public ActionResult GetFilters()
        {
            return PartialView("Videos/Filters", new VideosFiltersViewModel()
            {
                Channels = new List<string>()
                {
                    "dotNetConf",
                    "dotNetConfPL",
                    "NDC Oslo Conferences",
                    "tretton37"
                },
                PublicationYears = Enumerable.Range(2009, 5).ToList(),
                Tags = new List<string>()
                {
                    "C#",
                    ".NET",
                    "databases",
                    "ndcoslo",
                    "agile",
                    "software development",
                    "software architecture"
                }
            });
        }

        public ActionResult Query(FindVideosQuery query)
        {
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}