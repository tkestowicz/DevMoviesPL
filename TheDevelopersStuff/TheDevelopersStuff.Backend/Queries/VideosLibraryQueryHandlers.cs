using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.Model;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Queries
{
    public class VideosLibraryQueryHandlers : IQueryHandler<List<VideoViewModel>, FindVideosQuery>
    {
        private readonly MongoDatabase db;

        public VideosLibraryQueryHandlers(MongoDatabase db)
        {
            this.db = db;
        }

        public List<VideoViewModel> Handle(FindVideosQuery query)
        {
            Func<Channel, bool> name = vid =>
            {
                if (string.IsNullOrEmpty(query.ChannelName) == false)
                    return vid.Name == query.ChannelName;

                return true;
            };

            Func<Video, bool> publicationYear = vid =>
            {

                if (query.PublicationYear.HasValue)
                    return vid.PublicationDate.Year == query.PublicationYear;

                return true;
            };

            Func<Video, bool> tags = vid =>
            {
                if (query.Tags.Any())
                {
                    var r = vid
                        .Tags
                        .Select(t => t.Name)
                        .ContainsAny(query.Tags);

                    return r;
                }

                return true;
            };

            var videos = db.GetCollection<Video>("Videos")
                .FindAll()
                .Where(publicationYear)
                .Where(tags)
                .ToList();

            var channelsToFind = videos
                .Select(v => v.ChannelId)
                .Distinct()
                .ToArray();

            var channels = db.GetCollection<Channel>("Channels")
                .FindAll()
                .Where(c => channelsToFind.Contains(c.Id))
                .Where(name)
                .ToList();

            channelsToFind = channels
                .Select(v => v.Id)
                .Distinct()
                .ToArray();

            return videos
                .Where(v => channelsToFind.Contains(v.ChannelId))
                .ToViewModel(channels)
                .ToList();
        }
    }
}