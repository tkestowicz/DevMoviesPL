using System.Collections.Generic;
using TheDevelopersStuff.Backend.Providers;

namespace TheDevelopersStuff.Backend.ViewModels
{
    public class ConferenceViewModel
    {
        public ConferenceViewModel()
        {
            Videos = new List<VideoViewModel>();
        }

        public string Name { get; set; }

        public List<VideoViewModel> Videos { get; private set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
    }
}