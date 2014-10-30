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
            Func<Channel, bool> name = conf =>
            {
                if (string.IsNullOrEmpty(query.ChannelName) == false)
                    return conf.Name == query.ChannelName;

                return true;
            };

            Func<Video, bool> publicationYear = vid =>
            {

                if (query.PublicationYear.HasValue)
                    return vid.PublicationDate.Year == query.PublicationYear;

                return true;
            };

            var videos = db.GetCollection<Video>("Videos")
                .FindAll()
                .Where(publicationYear)
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