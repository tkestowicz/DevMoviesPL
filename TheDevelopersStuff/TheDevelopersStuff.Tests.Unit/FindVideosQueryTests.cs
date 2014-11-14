using System.Linq;
using Ploeh.AutoFixture;
using Should;
using TheDevelopersStuff.Backend.Queries;
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

            query.Tags.Count().ShouldEqual(expectedNumberOfTags);
        }
    }
}
