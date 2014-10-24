using System;
using System.Collections.Generic;
using System.Linq;
using TheDevelopersStuff.Backend.DataSources;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;
using Xunit;

namespace TheDevelopersStuff.Tests.Integration
{
    public class VideosLibraryQueriesTests
    {
        private TResult execute<TResult, TQuery>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            var handler = new VideosLibraryQueryHandlers(new VideosDataSource()) as IQueryHandler<TResult, TQuery>;

            if (handler == null)
                throw new ArgumentException("Query handler is not defined.");

            return handler.Handle(query);
        }


        [Fact]
        public void Query__default_filters_applied__returns_videos_from_library()
        {
            var result = execute<List<VideosViewModel>, FindVideosQuery>(new FindVideosQuery());

            Assert.NotEmpty(result);
        }

        [Fact]
        public void Query__ndc_filter_applied__returns_videos_from_ndc_only()
        {
            var result = execute<List<VideosViewModel>, FindVideosQuery>(new FindVideosQuery()
            {
                Conference = new ConferenceFilters
                {
                    Name = "NDC"
                }
            });

            var hasOnlyNDCResults = result.All(v => v.ConferenceName == "NDC");

            Assert.NotEmpty(result);
            Assert.True(hasOnlyNDCResults);
        }

    }
}
