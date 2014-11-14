using System.Collections.Generic;
using System.Linq;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Queries
{
    public class FlattenedVideosQuery
    {
        public string ChannelName { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public int? PublicationYear { get; set; }
        public int? Page { get; set; }
        public string PropertyName { get; set; }
        public OrderDirectionEnum Direction { get; set; }
    }

    public class FindVideosQuery : IQuery<List<VideoViewModel>>, IPagable, ISortable
    {
        private IEnumerable<string> tags;

        public FindVideosQuery()
        {
            Tags = new List<string>();
            Pagination = new PaginationSettings();

            OrderBy = new OrderSettings()
            {
                PropertyName = "PublicationDate",
                Direction = OrderDirectionEnum.Descending
            };
        }
        public string ChannelName { get; set; }
        public int? PublicationYear { get; set; }

        public IEnumerable<string> Tags {
            get { return tags; }
            set 
            {
                if (value == null)
                {
                    tags = null;
                    return;
                }

                tags = value
                    .Select(t => t.ToLower())
                    .Distinct(); 
            }
        }

        public PaginationSettings Pagination { get; set; }
        public OrderSettings OrderBy { get; set; }
    }
}