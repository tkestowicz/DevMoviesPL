using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.Providers;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Web.UI.Controllers
{
    public class VideosController : Controller
    {
        private readonly VideosLibraryQueryHandlers hander;
        private readonly VideosFiltersProvider filtersProvider;

        public VideosController()
        {
            var db = DatabaseConnectionFactory
                .CreateDatabaseConnection(ConfigurationManager.AppSettings["CurrentConnString"], ConfigurationManager.AppSettings["DevStuffDbName"]);

            hander = new VideosLibraryQueryHandlers(db);
            filtersProvider = new VideosFiltersProvider(db);
        }

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
            query.Pagination.PerPage = 12;

            var filters = filtersProvider.TakeAvailableFilters();
            var videos = hander.Handle(query);

            filters.Current = new VideosFiltersViewModel.SelectedFiltersViewModel
            {
                ChannelName = query.ChannelName,
                PublicationYear = query.PublicationYear,
                Tags = query.Tags
            };

            return View("Index", null, new VideosListViewModel
            {
                Query = query,
                Videos = videos,
                Filters = filters
            });
        }
    }

}