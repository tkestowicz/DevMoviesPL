﻿using System.Linq;
using Should;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.Providers;
using TheDevelopersStuff.Backend.ViewModels;
using TheDevelopersStuff.Tests.Integration.AutoData;
using Xunit;

namespace TheDevelopersStuff.Tests.Integration
{
    public class VideosFiltersTests : WithPopulatedMongoDb
    {

        private VideosFiltersViewModel execute()
        {
            return new VideosFiltersProvider(db)
                .TakeAvailableFilters();
        }

        [Fact]
        public void Channels__get_all__returns_list_of_available_channels()
        {
            var actualChannels = execute().Channels;

            var expectedChannels = channels
                .Select(c => c.Name)
                .OrderBy(c => c);

            actualChannels
                .SequenceEqual(expectedChannels)
                .ShouldBeTrue();
        }

        [Fact]
        public void Tags__get_all__returns_list_of_available_tags()
        {
            var actualTags = execute().Tags;

            var expectedTags = videos
                .SelectMany(v => v.Tags)
                .Select(t => t.Name)
                .OrderBy(t => t)
                .Distinct();

            actualTags
                .SequenceEqual(expectedTags)
                .ShouldBeTrue();
        }

        [Fact]
        public void PublicationYears__get_all__returns_list_of_available_publication_years()
        {
            var actualPublicationYears = execute().PublicationYears;

            var expectedPublicationYears = videos
                .Select(v => v.PublicationDate.Year)
                .Distinct()
                .OrderBy(y => y);

            actualPublicationYears
                .SequenceEqual(expectedPublicationYears)
                .ShouldBeTrue();
        }
    }
}