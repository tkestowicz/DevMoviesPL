using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheDevelopersStuff.Backend.DataSources;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Queries
{
    public class VideosLibraryQueryHandlers : IQueryHandler<List<ConferenceViewModel>, FindVideosQuery>
    {
        private readonly IVideosDataSource videosLibraryDataSource;

        public VideosLibraryQueryHandlers(IVideosDataSource videosLibraryDataSource)
        {
            this.videosLibraryDataSource = videosLibraryDataSource;
        }

        public List<ConferenceViewModel> Handle(FindVideosQuery query)
        {
            var results = videosLibraryDataSource.FindAll().GetAwaiter().GetResult();

            if (query.Conference != null && string.IsNullOrEmpty(query.Conference.Name) == false)
                results = results.Where(c => c.Name.ToLower().Contains(query.Conference.Name.ToLower())).ToList();

            return results;
        }
    }
}