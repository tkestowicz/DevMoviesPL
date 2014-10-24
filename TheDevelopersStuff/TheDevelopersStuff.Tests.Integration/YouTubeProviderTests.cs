using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TheDevelopersStuff.Backend.Providers;
using TheDevelopersStuff.Backend.ViewModels;
using Xunit;

namespace TheDevelopersStuff.Tests.Integration
{
    public class YouTubeProviderTests
    {
        [Fact]
        public async Task FindAll__no_filters_applied__returns_all_videos_from_channel()
        {
            var provider = new YouTubeProvider();
            var conferences = await provider.ChannelsInfo();

            Func<VideoViewModel, bool> expectedConditions = video =>
                string.IsNullOrEmpty(video.Url) == false &&
                string.IsNullOrEmpty(video.Name) == false &&
                string.IsNullOrEmpty(video.Id) == false;

            Assert.NotEmpty(conferences);
            conferences.TrueForAll(c => c.Videos.All(expectedConditions));
        }
    }
}