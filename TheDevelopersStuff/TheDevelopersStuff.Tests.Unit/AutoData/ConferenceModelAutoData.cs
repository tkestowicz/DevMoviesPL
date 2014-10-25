using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Tests.Unit.AutoData
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
}