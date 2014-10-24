using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Providers
{
    internal interface IVideoProvider
    {
        Task<List<ConferenceViewModel>> ChannelsInfo();
    }

    public class VideoViewModel
    {
        public string Url { get; internal set; }

        public string Id
        {
            get { return Url.Split('/').Last(); } 
        }

        public string Name { get; internal set; }
    }
}