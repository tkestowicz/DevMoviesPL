using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using MongoDB.Driver;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Should;
using TheDevelopersStuff.Backend.Model;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;
using TheDevelopersStuff.Tests.Integration.AutoData;
using TheDevelopersStuff.Tests.Integration.Extensions;
using TheDevelopersStuff.Tests.Integration.Fixtures;
using Xunit;
using Xunit.Extensions;

namespace TheDevelopersStuff.Tests.Integration
{
    public class VideosQueryHandlerTests : IUseFixture<MongoDbFixture>
    {
        private MongoDatabase db;
        private readonly IEnumerable<Video> videos;
        private readonly IEnumerable<Channel> channels;

        public VideosQueryHandlerTests()
        {
            var fixture = new Fixture()
                .Customize(new ModelAutoData.Customization());

            videos = fixture.Resolve<IEnumerable<Video>>();
            channels = fixture.Resolve<IEnumerable<Channel>>();
        }

        public VideosLibraryQueryHandlers create_handler()
        {
            return new VideosLibraryQueryHandlers(db);
        }

        [Fact]
        public void Query__no_filters_given__returns_corresponding_data()
        {
            var actualResult = create_handler().Handle(new FindVideosQuery());

            actualResult.ShouldEqual(videos.ToViewModel(channels));
        }

        [Fact]
        public void Query__name_filter_given__returns_corresponding_data()
        {
            var channelNameToQuery = channels.Last().Name;

            var actualResult = create_handler().Handle(new FindVideosQuery()
            {
                ChannelName = channelNameToQuery
            });

            Func<VideoViewModel, bool> matchesExpectedFilter
                = video => video.ChannelInfo.Name == channelNameToQuery;

            actualResult
                .All(matchesExpectedFilter)
                .ShouldBeTrue();
        }

        [Fact]
        public void Query__publication_year_filter_given__returns_corresponding_data()
        {
            var publicationYearToQuery = videos.First().PublicationDate.Year;

            var actualResult = create_handler().Handle(new FindVideosQuery()
            {
                PublicationYear = publicationYearToQuery
            });

            Func<VideoViewModel, bool> matchesExpectedFilter 
                = video => video.PublicationDate.Year == publicationYearToQuery;

            actualResult
                .All(matchesExpectedFilter)
                .ShouldBeTrue();
        }

        [Fact]
        public void Query__publication_year_and_name_filters_given__returns_corresponding_data()
        {
            var randomVideo = videos.First();
            var relatedChannel = channels.First(c => c.Id == randomVideo.ChannelId);

            var publicationYearFilterValue = randomVideo.PublicationDate.Year;
            var channelNameFilterValue = relatedChannel.Name;

            var actualResult = create_handler().Handle(new FindVideosQuery()
            {
                PublicationYear = publicationYearFilterValue,
                ChannelName = channelNameFilterValue
            });

            Func<VideoViewModel, bool> matchesExpectedFilters = video =>
            {
                var yearMatches = video.PublicationDate.Year == publicationYearFilterValue;
                var nameMatches = video.ChannelInfo.Name == channelNameFilterValue;

                return yearMatches && nameMatches;
            };

            actualResult
                .All(matchesExpectedFilters)
                .ShouldBeTrue();
        }

        public void SetFixture(MongoDbFixture data)
        {
            db = data.Db;
            data.Reset();

            db.GetCollection("Videos").InsertBatch(videos);
            db.GetCollection("Channels").InsertBatch(channels);
        }
    }

    
}
