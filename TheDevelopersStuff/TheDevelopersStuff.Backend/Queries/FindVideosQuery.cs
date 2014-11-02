using System.Collections.Generic;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.Model;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Queries
{
    public class FindVideosQuery : IQuery<List<VideoViewModel>>
    {
        public FindVideosQuery()
        {
            Tags = new List<string>();
        }
        public string ChannelName { get; set; }
        public int? PublicationYear { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}