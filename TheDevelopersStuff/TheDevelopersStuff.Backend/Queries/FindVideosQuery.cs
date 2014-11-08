using System;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.Model;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Queries
{
    public class FindVideosQuery : IQuery<List<VideoViewModel>>, IPagable, ISortable
    {
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
        public IEnumerable<string> Tags { get; set; }

        public PaginationSettings Pagination { get; set; }
        public OrderSettings OrderBy { get; set; }
    }
}