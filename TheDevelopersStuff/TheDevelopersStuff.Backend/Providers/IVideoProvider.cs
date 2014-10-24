using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheDevelopersStuff.Backend.Providers
{
    internal interface IVideoProvider
    {
        Task<List<VideoViewModel>> FindAll();
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