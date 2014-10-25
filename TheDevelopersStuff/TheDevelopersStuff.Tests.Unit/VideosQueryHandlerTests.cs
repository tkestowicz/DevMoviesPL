using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Xunit;
using Should;
using Simple.Data;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;
using TheDevelopersStuff.Tests.Unit.AutoData;
using TheDevelopersStuff.Tests.Unit.Fixtures;
using Xunit;
using Xunit.Extensions;

namespace TheDevelopersStuff.Tests.Unit
{
    public class VideosQueryHandlerTests : IUseFixture<InMemoryDbFixture>
    {
        private readonly dynamic db;
        private readonly VideosLibraryQueryHandlers handler;

        public VideosQueryHandlerTests()
        {
            db = Database.Open();
            handler = new VideosLibraryQueryHandlers(db);
        }

        [Theory, ConferenceModelAutoData]
        public void Query__no_filters_given__returns_corresponding_data(IEnumerable<ConferenceViewModel> conferences)
        {
            db.Channels.Insert(conferences);

            var actualResult = handler.Handle(new FindVideosQuery());

            actualResult
                .Select(c => c.Id)
                .ToArray()
                .ShouldEqual(conferences
                                    .Select(c => c.Id)
                                    .ToArray()
                );
        }

        [Theory, ConferenceModelAutoData]
        public void Query__name_filter_given__returns_corresponding_data(IEnumerable<ConferenceViewModel> conferences)
        {
            db.Channels.Insert(conferences);

            var conferenceNameToQuery = conferences.Last().Name;

            var actualResult = handler.Handle(new FindVideosQuery()
            {
                ChannelName = conferenceNameToQuery
            });

            actualResult
                .Select(c => c.Id)
                .ToArray()
                .ShouldEqual(conferences
                                    .Where(c => c.Name == conferenceNameToQuery)
                                    .Select(c => c.Id)
                                    .ToArray()
                );
        }

        public void SetFixture(InMemoryDbFixture data)
        {
        }
    }
}
