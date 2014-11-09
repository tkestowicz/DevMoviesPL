using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheDevelopersStuff.Backend.DataSources.DTO;
using TheDevelopersStuff.Backend.Providers;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.DataSources
{
    public class VideosDataSource : IVideosDataSource
    {
        private readonly IReadOnlyList<IVideoProvider> providers;

        public VideosDataSource()
            : this(new List<IVideoProvider>()
            {
                new VimeoProvider(),
                new YouTubeProvider()
            })
        {
            
        }

        public VideosDataSource(IReadOnlyList<IVideoProvider> videoProviders)
        {
            providers = videoProviders;
        }

        public async Task<List<ChannelDTO>> FindAll()
        {
            var results = new List<ChannelDTO>();

            foreach (var provider in providers)
            {
                results.AddRange(await provider.ChannelsData());
            }

            return results;
        }
    }
}