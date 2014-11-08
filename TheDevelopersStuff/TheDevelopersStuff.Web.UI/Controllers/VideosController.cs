using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Web.UI.Controllers
{
    public class VideosListViewModel
    {
        public List<VideoViewModel> Videos { get; set; }
        public VideosFiltersViewModel Filters { get; set; }
        public FindVideosQuery Query { get; set; }
    }

    public class VideosController : Controller
    {
        public ActionResult Index(FindVideosQuery query)
        {
            return View("Index", null, new VideosListViewModel
            {
                Query = query,
                Videos = new List<VideoViewModel>(),
                Filters = new VideosFiltersViewModel()
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
                },
                    Current = new VideosFiltersViewModel.SelectedFiltersViewModel
                    {
                        ChannelName = query.ChannelName,
                        PublicationYear = query.PublicationYear,
                        Tags = query.Tags
                    }
                }
            });
        }
    }

}