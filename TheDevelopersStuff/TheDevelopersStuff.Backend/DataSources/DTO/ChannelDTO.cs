using System.Collections.Generic;

namespace TheDevelopersStuff.Backend.DataSources.DTO
{
    public class ChannelDTO
    {
        public ChannelDTO()
        {
            Videos = new List<Backend.ViewModels.VideoViewModel>();
        }

        public string Name { get; set; }

        public List<Backend.ViewModels.VideoViewModel> Videos { get; private set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Id { get; set; }
    }
}