using System.Collections.Generic;
using TheDevelopersStuff.Backend.Providers;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.DataSources
{
    internal class VideosDataSource : IVideosDataSource
    {
        private readonly IReadOnlyList<IVideoProvider> providers = new List<IVideoProvider>();

        public List<VideosViewModel> FindAll(FindVideosQuery query)
        {
            var results = new List<VideosViewModel>();

            foreach (var provider in providers)
            {
                //TODO: Add query within providers
            }

            return results;
        }
    }
}