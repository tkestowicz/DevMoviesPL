﻿using System.Linq;
using Ploeh.AutoFixture;
using Should;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;
using Xunit;

namespace TheDevelopersStuff.Tests.Unit
{
    public class FindVideosQueryTests
    {
        [Fact]
        public void QueryObject__tags_null_collection_given__tags_property_is_null()
        {
            var query = new FindVideosQuery();

            query.Tags = null;

            query.Tags.ShouldBeNull();
        }

        [Fact]
        public void QueryObject__similar_tags_given__returns_collection_without_duplicates()
        {
            var randomString = new Fixture().Create<string>();

            var query = new FindVideosQuery()
            {
                Tags = new[]
                {
                    randomString.ToUpper(),
                    randomString.ToLower()
                }
            };

            const int expectedNumberOfTags = 1;

            query
                .Tags
                .Count()
                .ShouldEqual(expectedNumberOfTags);
        }

        [Fact]
        public void QueryObject__current_page_is_over_the_limit__set_current_page_as_the_last()
        {
            var query = new FindVideosQuery();

            query.Pagination.NumberOfRecords = 10;
            query.Pagination.PerPage = 5;

            query.Pagination.Page = 3;

            const int expectedPage = 2;

            query
                .Pagination
                .Page
                .ShouldEqual(expectedPage);
        }

        [Fact]
        public void QueryObject__current_page_is_under_the_limit__set_current_page_as_the_first()
        {
            var query = new FindVideosQuery();

            query.Pagination.NumberOfRecords = 10;
            query.Pagination.PerPage = 5;

            query.Pagination.Page = 0;

            const int expectedPage = 1;

            query
                .Pagination
                .Page
                .ShouldEqual(expectedPage);
        }

        [Fact]
        public void QueryObject__property_name_is_null__default_value_used()
        {
            var query = new FindVideosQuery();

            var expectedValue = query.OrderBy.PropertyName;

            query.OrderBy.PropertyName = null;
            
            query
                .OrderBy
                .PropertyName
                .ShouldEqual(expectedValue);
        }

        [Fact]
        public void QueryObject__new_object_created__property_name_has_default_value()
        {
            var query = new FindVideosQuery();

            query
                .OrderBy
                .PropertyName
                .ShouldNotBeNull();
        }

        [Fact]
        public void QueryObject__null_and_empty_tags_given__tags_rejected()
        {
            var query = new FindVideosQuery();

            query.Tags = new[] { null, string.Empty };

            const int expectedNumberOfTags = 0;

            query
                .Tags
                .Count()
                .ShouldEqual(expectedNumberOfTags);
        }
    }
}