using System.Collections.Generic;
using TheDevelopersStuff.Backend.Queries;

namespace TheDevelopersStuff.Backend.ViewModels
{
    public class VideosListViewModel
    {
        public List<VideoViewModel> Videos { get; set; }
        public VideosFiltersViewModel Filters { get; set; }
        public FindVideosQuery Query { get; set; }
    }
}