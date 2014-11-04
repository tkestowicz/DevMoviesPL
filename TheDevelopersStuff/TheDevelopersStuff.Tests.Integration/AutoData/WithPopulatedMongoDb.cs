using System.Collections.Generic;
using MongoDB.Driver;
using Ploeh.AutoFixture;
using TheDevelopersStuff.Backend.Model;
using TheDevelopersStuff.Tests.Integration.Extensions;
using TheDevelopersStuff.Tests.Integration.Fixtures;
using Xunit;

namespace TheDevelopersStuff.Tests.Integration.AutoData
{
    public abstract class WithPopulatedMongoDb : IUseFixture<MongoDbFixture>
    {
        protected MongoDatabase db;

        protected readonly IEnumerable<Video> videos;
        protected readonly IEnumerable<Channel> channels;

        protected WithPopulatedMongoDb()
        {
            var fixture = new Fixture()
                .Customize(new ModelAutoData.Customization());

            videos = fixture.Resolve<IEnumerable<Video>>();
            channels = fixture.Resolve<IEnumerable<Channel>>();
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