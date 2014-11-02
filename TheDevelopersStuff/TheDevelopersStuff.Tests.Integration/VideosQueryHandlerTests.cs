using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
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

            actualResult.ShouldEqual(videos.TransformToExpectedViewModel(channels));
        }

        [Fact]
        public void Query__query_object_not_given__uses_default_settings()
        {
            var actualResult = create_handler().Handle(null);

            actualResult.ShouldEqual(videos.TransformToExpectedViewModel(channels));
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

        [Fact]
        public void Query__tags_filter_given__returns_matching_results()
        {
            var tags = videos
                .First()
                .Tags
                .ToList();

            tags.AddRange(videos.Last().Tags);

            var expectedTags = tags.Distinct().Select(t => t.Name);

            var actualResult = create_handler().Handle(new FindVideosQuery()
            {
                Tags = expectedTags
            });

            actualResult.ForEach(v =>
            {
                v.Tags
                    .Any(t => expectedTags.Contains(t))
                    .ShouldBeTrue("Video does not contain any of expected tags.");
            });
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineAutoData(2, 2)]
        [InlineAutoData(3, 1)]
        public void Query__page_and_per_page_filters_set__returns_appropriate_number_of_records(int page, int perPage)
        {
            var actualResult = create_handler().Handle(new FindVideosQuery()
            {
                Pagination = new PaginationSettings()
                {
                    Page = page,
                    PerPage = perPage
                }
            });

            var expectedVideos = videos
                .Skip((page - 1)*perPage)
                .Take(perPage);

            actualResult.ShouldEqual(expectedVideos.TransformToExpectedViewModel(channels));
        }

        [Fact]
        public void Query__page_and_per_page_not_given__uses_default_settings()
        {
            const int defaultPage = 1;
            const int defaultPerPage = 10;

            var actualResult = create_handler().Handle(new FindVideosQuery());

            var expectedVideos = videos
                .Skip((defaultPage - 1) * defaultPerPage)
                .Take(defaultPerPage);

            actualResult.ShouldEqual(expectedVideos.TransformToExpectedViewModel(channels));
        }

        [Fact]
        public void Query__order_not_set__uses_default_order()
        {
            var actualResult = create_handler().Handle(new FindVideosQuery());

            var expectedOrder = videos
                .TransformToExpectedViewModel(channels)
                .OrderByDescending(v => v.PublicationDate)
                .Select(v => v.Id)
                .ToArray();

            actualResult
                .Select(v => v.Id)
                .ToArray()
                .ShouldEqual(expectedOrder);
        }

        [Fact]
        public void Query__order_ascending_by_name__result_is_sorted()
        {
            var orderBy = new OrderSettings()
            {
                PropertyName = "Title",
                Direction = OrderByDirection.Ascending
            };
            
            var expectedOrder = videos
                .TransformToExpectedViewModel(channels)
                .OrderBy(v => v.Title);

            execute_sort(orderBy, expectedOrder);
        }

        [Fact]
        public void Query__order_ascending_by_likes__result_is_sorted()
        {
            var orderBy = new OrderSettings()
                {
                    PropertyName = "Likes",
                    Direction = OrderByDirection.Ascending
                };

            var expectedOrder = videos
                .TransformToExpectedViewModel(channels)
                .OrderBy(v => v.Likes);

            execute_sort(orderBy, expectedOrder);
        }

        [Fact]
        public void Query__order_ascending_by_channel_name__result_is_sorted()
        {
            var orderBy = new OrderSettings()
            {
                PropertyName = "ChannelInfo.Name",
                Direction = OrderByDirection.Ascending
            };

            var expectedOrder = videos
                .TransformToExpectedViewModel(channels)
                .OrderBy(v => v.ChannelInfo.Name);

            execute_sort(orderBy, expectedOrder);
        }

        private void execute_sort(OrderSettings orderBy, IEnumerable<VideoViewModel> expectedOrder)
        {
            var actualResult = create_handler().Handle(new FindVideosQuery()
            {
                OrderBy = orderBy
            });

            var expected = expectedOrder
                .Select(v => v.Id)
                .ToArray();
            
            actualResult
                .Select(v => v.Id)
                .ToArray()
                .ShouldEqual(expected);
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
