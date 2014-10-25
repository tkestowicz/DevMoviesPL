using System.Collections.Generic;
using System.Threading.Tasks;
using TheDevelopersStuff.Backend.Infrastructure;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Queries
{
    public class VideosLibraryQueryHandlers : IQueryHandler<List<ConferenceViewModel>, FindVideosQuery>
    {
        private readonly dynamic db;

        public VideosLibraryQueryHandlers(dynamic db)
        {
            this.db = db;
        }

        public List<ConferenceViewModel> Handle(FindVideosQuery query)
        {
            if (string.IsNullOrEmpty(query.ChannelName) == false)
                return db.Channels.FindAllByName(query.ChannelName);

            return db.Channels.All();
        }
    }
}