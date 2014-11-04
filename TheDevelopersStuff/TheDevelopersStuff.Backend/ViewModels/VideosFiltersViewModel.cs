using System.Collections.Generic;

namespace TheDevelopersStuff.Backend.ViewModels
{
    public class VideosFiltersViewModel
    {
        public VideosFiltersViewModel()
        {
            Channels = new List<string>();
            Tags = new List<string>();
            PublicationYears = new List<int>();
        }
        public List<string> Channels { get; set; }
        public List<string> Tags { get; set; }
        public List<int> PublicationYears { get; set; }
    }
}