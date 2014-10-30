using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using TheDevelopersStuff.Backend.Model;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Tests.Integration.AutoData
{
    public class ConferenceModelAutoData : AutoDataAttribute
    {
        public class Customization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                Func<ConferenceViewModel> conference = () => fixture.Build<ConferenceViewModel>()
                    .Do(c => c.Videos.AddRange(fixture.CreateMany<VideoViewModel>()))
                    .Create();

                fixture.Register(conference);
            }
        }

        public ConferenceModelAutoData()
            : base(new Fixture().Customize(new Customization()))
        {
            
        }
    }

    public class ModelAutoData : AutoDataAttribute
    {
        public class Customization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var channels = fixture.Build<Channel>()
                                .CreateMany();

                var channelIds = channels.Select(v => v.Id);

                var videos = fixture.Build<Video>()
                    .With(c => c.ChannelId, channelIds.ElementAt(FakeData.NumberData.GetNumber(0, channelIds.Count() - 1)))
                    .CreateMany(FakeData.NumberData.GetNumber(3, 12));

                fixture.Register(() => videos);
                fixture.Register(() => channels);
            }
        }

        public ModelAutoData()
            : base(new Fixture().Customize(new Customization()))
        {

        }
    }
}