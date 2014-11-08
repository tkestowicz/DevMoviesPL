using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using TheDevelopersStuff.Backend.Extensions;
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
            if(query == null)
                query = new FindVideosQuery();

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
                    return vid
                        .Tags
                        .Select(t => t.Name)
                        .ContainsAny(query.Tags);

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

            var partialResult = videos
                .Where(v => channelsToFind.Contains(v.ChannelId));

            query.Pagination.NumberOfRecords = partialResult.Count();

            return partialResult
                .Skip((query.Pagination.Page - 1)*query.Pagination.PerPage)
                .Take(query.Pagination.PerPage)
                .ToViewModel(channels)
                .AsQueryable()
                .OrderBy(query.OrderBy.PropertyName, query.OrderBy.Direction)
                .ToList();
        }
    }
}