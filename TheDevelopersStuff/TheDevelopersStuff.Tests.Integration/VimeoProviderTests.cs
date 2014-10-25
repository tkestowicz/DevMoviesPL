using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Should;
using TheDevelopersStuff.Backend.Providers;
using TheDevelopersStuff.Backend.ViewModels;
using TheDevelopersStuff.Tests.Integration.Extensions;
using TheDevelopersStuff.Tests.Integration.Fixtures;
using Xunit;

namespace TheDevelopersStuff.Tests.Integration
{
    public class VimeoProviderTests : IUseFixture<VideoProvidersFixture>
    {
        IVideoProvider provider;

        [Fact]
        public async Task FindAll__no_filters_given__returns_all_videos()
        {
            var conferences = await provider.ChannelsData();

            conferences.ShouldBeFilledCorrectly();
        }

        public void SetFixture(VideoProvidersFixture data)
        {
            provider = data.VimeoProvider;
        }
    }
}
