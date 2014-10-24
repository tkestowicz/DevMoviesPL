using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheDevelopersStuff.Backend.Providers;
using TheDevelopersStuff.Backend.ViewModels;
using Xunit;

namespace TheDevelopersStuff.Tests.Integration
{
    public class VimeoProviderTests
    {
        readonly IVideoProvider provider = new VimeoProvider();

        [Fact]
        public async Task FindAll__no_filters_given__returns_all_videos()
        {
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
