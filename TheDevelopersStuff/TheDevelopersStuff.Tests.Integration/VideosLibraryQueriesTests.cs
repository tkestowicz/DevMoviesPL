using System;
using System.Collections.Generic;
using System.Linq;
using Should;
using TheDevelopersStuff.Backend.DataSources;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.Providers;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;
using TheDevelopersStuff.Tests.Integration.Extensions;
using TheDevelopersStuff.Tests.Integration.Fixtures;
using Xunit;
using Xunit.Extensions;

namespace TheDevelopersStuff.Tests.Integration
{
    public class VideosLibraryQueriesTests : IUseFixture<VideoProvidersFixture>
    {

        private IVideosDataSource videosDataSource;

       private TResult execute<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var handler = new VideosLibraryQueryHandlers(videosDataSource) as IQueryHandler<TResult, TQuery>;

            if (handler == null)
                throw new ArgumentException("Query handler is not defined.");

            return handler.Handle(query);
        }


        [Fact]
        public void Query__default_filters_applied__returns_videos_from_library()
        {
            var conferences = execute<List<ConferenceViewModel>, FindVideosQuery>(new FindVideosQuery());

            conferences.ShouldBeFilledCorrectly();
        }

        [Theory]
        [InlineData("Jake Fried")]
        [InlineData("dotNetConf")]
        public void Query__conference_name_filter_applied__returns_videos_only_from_expected_conf(string conferenceName)
        {
            var result = execute<List<ConferenceViewModel>, FindVideosQuery>(new FindVideosQuery()
            {
                Conference = new ConferenceFilters
                {
                    Name = conferenceName
                }
            });

            var hasOnlyExpectedResults = result.All(v => v.Name.ToLower().Contains(conferenceName.ToLower()));

            result.ShouldNotBeEmpty();
            hasOnlyExpectedResults.ShouldBeTrue();
        }

        public void SetFixture(VideoProvidersFixture data)
        {
            videosDataSource = data.DataSource;
        }
    }
}
