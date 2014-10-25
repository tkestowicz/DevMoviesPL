using System.Collections.Generic;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Queries
{
    public class FindVideosQuery : IQuery<List<ConferenceViewModel>>
    {
        public string ChannelName { get; set; }
    }
}