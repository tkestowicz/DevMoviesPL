using System.Collections.Generic;
using TheDevelopersStuff.Backend.DataSources;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Queries
{
    public class VideosLibraryQueryHandlers : IQueryHandler<List<VideosViewModel>, FindVideosQuery>
    {
        private readonly IVideosDataSource videosLibraryDataSource;

        public VideosLibraryQueryHandlers(IVideosDataSource videosLibraryDataSource)
        {
            this.videosLibraryDataSource = videosLibraryDataSource;
        }

        public List<VideosViewModel> Handle(FindVideosQuery query)
        {
            return videosLibraryDataSource.FindAll(query);
        }
    }
}