using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Providers
{
    internal class YouTubeProvider : IVideoProvider
    {
        private readonly YouTubeConfig config;
        private readonly IReadOnlyCollection<string> channels;

        internal class YouTubeConfig
        {
            internal string AppKey
            {
                get { return "AIzaSyCHpg6UHLqfmMb63h-n_T6zus7jWFGRMMM"; }
            }

            internal string ChannelUri(string channelId)
            {
                return string.Format("https://www.youtube.com/channel/{0}", channelId);
            }

            internal string VideoUri(string videoId)
            {
                return string.Format("https://www.youtube.com/watch?v={0}", videoId);
            }
        }

        private readonly YouTubeService service;

        internal YouTubeProvider()
            : this(new YouTubeConfig(), new List<string>()
            {
                "ABBDevDay",
                "dotnetConf",
                "UCs3oPPpRdETQTsxVF-Wvqbg" // dotnetconfpl
            })
        {
        }

        public YouTubeProvider(YouTubeConfig config, IReadOnlyCollection<string> channels)
        {
            this.config = config;
            this.channels = channels;

            service = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = config.AppKey
            });
        }


        public async Task<List<ConferenceViewModel>> ChannelsInfo()
        {
            var results = new List<ConferenceViewModel>();

            foreach (var channel in channels)
            {
                var conference = new ConferenceViewModel();

                var info = await ChannelInfo(channel);

                if (info == null)
                    continue;

                conference.Name = info.Snippet.Title;
                conference.Description = info.Snippet.Description;
                conference.Link = config.ChannelUri(info.Id);

                conference.Videos.AddRange(await ChannelVideos(info.Id));

                results.Add(conference);
            }

            return results;
        }

        private async Task<List<VideoViewModel>>  ChannelVideos(string channelId)
        {
            var results = new List<VideoViewModel>();

            var s = service.Search.List("snippet");

            s.ChannelId = channelId;
            s.Type = "video";
            s.MaxResults = 50;

            string nextPage = null;

            do
            {
                s.PageToken = nextPage;

                var videos = await s.ExecuteAsync();

                foreach (var video in videos.Items)
                {
                    if (video.Id.Kind != "youtube#video")
                        continue;

                    results.Add(new VideoViewModel()
                    {
                        Name = video.Snippet.Title,
                        Url = config.VideoUri(video.Id.VideoId),
                        Id = video.Id.VideoId
                    });
                }

                nextPage = videos.NextPageToken;
            } while (string.IsNullOrEmpty(nextPage) == false);

            return results;
        }

        private async Task<Channel> ChannelInfo(string channel)
        {
            var request = service.Channels.List("snippet");

            request.ForUsername = channel;

            var response = await request.ExecuteAsync();

            if (response.PageInfo.TotalResults != 0) 
                return response.Items.FirstOrDefault();

            request.ForUsername = null;
            request.Id = channel;

            response = await request.ExecuteAsync();

            return response.Items.FirstOrDefault();
        }
    }
}