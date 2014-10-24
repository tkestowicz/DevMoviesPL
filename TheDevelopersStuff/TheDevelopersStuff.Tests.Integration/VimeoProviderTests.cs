using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheDevelopersStuff.Backend.Providers;
using Xunit;

namespace TheDevelopersStuff.Tests.Integration
{
    public class VimeoProviderTests
    {
        readonly IVideoProvider provider = new VimeoProvider();

        [Fact]
        public async Task FindAll__no_filters_given__returns_all_videos()
        {
            var videos = await provider.FindAll();

            Func<VideoViewModel, bool> expectedConditions = video => 
                string.IsNullOrEmpty(video.Url) == false &&
                string.IsNullOrEmpty(video.Name) == false &&
                string.IsNullOrEmpty(video.Id) == false;

            Assert.NotEmpty(videos);
            Assert.True(videos.All(expectedConditions));
        }
    }

    internal class VimeoProvider : IVideoProvider
    {
        internal class VimeoConfig
        {
            internal Uri ApiUri
            {
                get { return new Uri("https://api.vimeo.com"); }
            }

            internal string Version
            {
                get { return "3.2"; }
            }

            internal string AcceptHeader
            {
                get { return string.Format("application/vnd.vimeo.*+json;version={0}", Version); }
            }

            internal AuthenticationHeaderValue AuthorizationHeader
            {
                get
                {
                    return new AuthenticationHeaderValue("bearer", "75352f15477eb0a2b53d4dbef76a4da6");
                }
            }

            internal Uri ChannelsUri(string username)
            {
                return new Uri(ApiUri, string.Format("users/{0}/channels", username));
            }

            internal Uri VideosUri(string username, int page = 1)
            {
                return new Uri(ApiUri, string.Format("users/{0}/videos?page={1}&per_page=50", username, page));
            }
        }

        private readonly VimeoConfig config;

        readonly IReadOnlyCollection<string> accounts;

        private readonly Func<VimeoConfig, HttpClient> webClientFactory = cfg =>
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = cfg.AuthorizationHeader;

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", cfg.AcceptHeader);

            return client;
        };

        public VimeoProvider()
            : this(new VimeoConfig(), new ReadOnlyCollection<string>(new List<string>(1)
        {
            "user14410096", //tretton37
            "ndcoslo"
        }))
        {
            
        }

        public VimeoProvider(VimeoConfig config, IReadOnlyCollection<string> accounts)
        {
            this.config = config;
            this.accounts = accounts;
        }

        public async Task<List<VideoViewModel>> FindAll()
        {
            var results = new List<VideoViewModel>();

            foreach (var account in accounts)
            {
                results.AddRange(await GetAllVideos(account));
            }

            return results;
        }

        private async Task<List<VideoViewModel>> GetAllVideos(string user)
        {
            using (var client = webClientFactory(config))
            {
                var results = new List<VideoViewModel>(500);

                var nextPage = string.Empty;
                do
                {
                    var uri = string.IsNullOrEmpty(nextPage)
                        ? config.VideosUri(user)
                        : new Uri(config.ApiUri, nextPage);

                    var response = await client.GetAsync(uri);

                    nextPage = await ProcessResult(response.Content, results);

                } while (nextPage != null);

                return results;
            }
        } 

        private static async Task<string> ProcessResult(HttpContent content, ICollection<VideoViewModel> videos)
        {
            if (videos == null) throw new ArgumentNullException("videos");

            dynamic result = JsonConvert.DeserializeObject(await content.ReadAsStringAsync());

            //TODO: Add logger here
            if (string.IsNullOrEmpty(result.error) == false)
                return null;

            foreach (var video in result.data)
            {
                videos.Add(new VideoViewModel
                {
                    Url = video.link,
                    Name = video.name
                });
            }

            return result.paging.next;
        }

    }
}
