using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Web.UI.Controllers
{
    public class VideosController : Controller
    {
        public ActionResult Index(FlattenedVideosQuery @params)
        {
            var query = new FindVideosQuery()
            {
                ChannelName = @params.ChannelName,
                PublicationYear = @params.PublicationYear,
                Tags = @params.Tags
            };

            if (@params.Tags != null && @params.Tags.Any())
                query.Tags = @params.Tags.Where(t => !string.IsNullOrEmpty(t));

            if (string.IsNullOrEmpty(@params.PropertyName) == false)
            {
                query.OrderBy.Direction = @params.Direction;
                query.OrderBy.PropertyName = @params.PropertyName;   
            }

            query.Pagination.Page = @params.Page ?? 1;

            return View("Index", null, new VideosListViewModel
            {
                Query = query,
                Videos = new List<VideoViewModel>()
                {
                    new VideoViewModel()
                    {
                        Title = "Luke Wroblewski - It’s a Write/Read (Mobile) Web",
                        Views = 123,
                        Likes = 12,
                        Dislikes = 0,
                        PublicationDate = DateTime.Now,
                        Id = "110100738",
                        Url = "http://vimeo.com/110100738",
                        Description = "On the surface, content is king online. But digging deeper into the underbelly of the web reveals a complex ecosystem of communication and contribution that shapes the web and how we interact with it. What lessons can we learn from the web’s inner workings as we move to a mobile-driven, multi-device internet? Luke will not only lift the covers on where we need to focus our efforts but share lots of practical advice on how as well.",
                        ChannelInfo = new ChannelInfoViewModel()
                        {
                            Link = "http://vimeo.com/",
                            Name = "dotNetConf"
                        },
                        Tags = new List<string>()
                        {
                            "C#", "Default"
                        }
                    },
                    new VideoViewModel()
                    {
                        Title = "Luke Wroblewski - It’s a Write/Read (Mobile) Web",
                        Views = 123,
                        Likes = 12,
                        Dislikes = 0,
                        PublicationDate = DateTime.Now,
                        Id = "Pi3bc9lS3rg",
                        Url = "https://www.youtube.com/embed/Pi3bc9lS3rg",
                        Description = "On the surface, content is king online. But digging deeper into the underbelly of the web reveals a complex ecosystem of communication and contribution that shapes the web and how we interact with it. What lessons can we learn from the web’s inner workings as we move to a mobile-driven, multi-device internet? Luke will not only lift the covers on where we need to focus our efforts but share lots of practical advice on how as well.",
                        ChannelInfo = new ChannelInfoViewModel()
                        {
                            Link = "http://youtube.com/",
                            Name = "ndcoslo"
                        },
                        Tags = new List<string>()
                        {
                            "C#", "Default"
                        }
                    }
                },
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