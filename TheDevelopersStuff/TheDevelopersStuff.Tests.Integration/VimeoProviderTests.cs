using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
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
        public async Task ChannelsData__explore_channel__returns_complete_info()
        {
            var conferences = await provider.ChannelsData();

            // Vimeo doesn't provide dislikes
            conferences.ForEach(c => c.Videos.ForEach(v => v.Dislikes = new Fixture().Create<int>()));

            conferences.ShouldBeFilledCorrectly();
        }

        public void SetFixture(VideoProvidersFixture data)
        {
            provider = data.VimeoProvider;
        }
    }
}
