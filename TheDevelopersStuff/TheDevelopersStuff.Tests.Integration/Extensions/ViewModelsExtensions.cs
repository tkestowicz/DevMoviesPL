using System.Collections.Generic;
using Should;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Tests.Integration.Extensions
{
    public static class ViewModelsExtensions
    {
        public static void ShouldBeFilledCorrectly(this List<ConferenceViewModel> conferences)
        {
            conferences.ShouldNotBeEmpty();
            conferences.ForEach(ShouldBeFilledCorrectly);
        }

        public static void ShouldBeFilledCorrectly(this ConferenceViewModel conference)
        {
            conference.Name.ShouldNotBeEmpty();
            conference.Description.ShouldNotBeNull();
            conference.Link.ShouldNotBeEmpty();
            conference.Videos.ShouldNotBeEmpty();

            conference.Videos.ForEach(ShouldBeFilledCorrectly);
        }

        public static void ShouldBeFilledCorrectly(this VideoViewModel video)
        {
            video.Id.ShouldNotBeEmpty();
            video.Name.ShouldNotBeEmpty();
            video.Url.ShouldNotBeEmpty();
        }
    }
}