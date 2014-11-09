using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TheDevelopersStuff.Backend.DataSources;
using TheDevelopersStuff.Backend.Model;
using TheDevelopersStuff.Backend.Providers;

namespace TheDevelopersStuff.Videos.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Setup migration ...");

                var client = new MongoClient(ConfigurationManager.ConnectionStrings["DevStuff"].ConnectionString);

                var db = client.GetServer().GetDatabase(ConfigurationManager.AppSettings["DevStuffDbName"]);

                var existingVideos = db.GetCollection<Video>("Videos")
                    .AsQueryable()
                    .Select(v => new {v.Id, v.ChannelId})
                    .ToList();

                var existingChannels = existingVideos
                    .Select(v => v.ChannelId)
                    .Distinct()
                    .ToList();

                var vimeo = ConfigurationManager.AppSettings["VimeoAccounts"].Split(',');
                var yt = ConfigurationManager.AppSettings["YoutubeAccounts"].Split(',');

                var datasource = new VideosDataSource(new List<IVideoProvider>(2)
                {
                    new VimeoProvider(new VimeoProvider.VimeoConfig(), vimeo),
                    new YouTubeProvider(new YouTubeProvider.YouTubeConfig(), yt),
                });

                Console.WriteLine("Importing data from external resources ...");

                var allEntries = datasource
                    .FindAll()
                    .GetAwaiter()
                    .GetResult()
                    .Where(c => existingChannels.Contains(c.Id) == false);

                Console.WriteLine("Map data to internal structure ...");

                var channels = new List<Channel>();
                var videos = new List<Video>();

                foreach (var channelDto in allEntries)
                {
                    channels.Add(new Channel
                    {
                        Description = channelDto.Description,
                        Id = channelDto.Id,
                        Link = channelDto.Link,
                        Name = channelDto.Name
                    });

                    var cid = channelDto.Id;

                    var channelVideos =
                        channelDto.Videos
                        .Where(v => existingVideos.Select(vid => vid.Id).Contains(v.Id) == false)
                        .Select(v => new Video()
                        {
                            ChannelId = cid,
                            Id = v.Id,
                            Description = v.Description,
                            Link = v.Url,
                            PublicationDate = v.PublicationDate,
                            Stats = new Statistics()
                            {
                                Dislikes = v.Dislikes,
                                Likes = v.Likes,
                                Views = v.Views
                            },
                            Tags = v.Tags.Select(t => new Tag()
                            {
                                Name = t,
                                Slug = t
                            }).ToArray(),
                            Title = v.Title
                        });

                    videos.AddRange(channelVideos);
                }

                Console.WriteLine("Save data ...");

                db.GetCollection("Channels").InsertBatch(channels);
                db.GetCollection("Videos").InsertBatch(videos);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Migration completed.");
                Console.WriteLine("Imported {0} channels and {1} videos.", channels.Count, videos.Count);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message+Environment.NewLine);
                Console.WriteLine(e.StackTrace);
                Console.ReadLine(); 
            }
        }
    }
}
