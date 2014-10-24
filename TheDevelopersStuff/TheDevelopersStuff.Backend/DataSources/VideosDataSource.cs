using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheDevelopersStuff.Backend.Providers;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.DataSources
{
    internal class VideosDataSource : IVideosDataSource
    {
        private readonly IReadOnlyList<IVideoProvider> providers;

        public VideosDataSource()
            : this(new List<IVideoProvider>()
            {
                new VimeoProvider()
            })
        {
            
        }

        public VideosDataSource(IReadOnlyList<IVideoProvider> videoProviders)
        {
            providers = videoProviders;
        }

        public async Task<List<ConferenceViewModel>> FindAll(FindVideosQuery query)
        {
            var results = new List<ConferenceViewModel>();

            foreach (var provider in providers)
            {
                results.AddRange(await provider.ChannelsInfo());
            }

            return results;
        }
    }
}