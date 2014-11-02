using MongoDB.Driver.Linq;

namespace TheDevelopersStuff.Backend.Queries
{
    public class OrderSettings
    {
        public string PropertyName { get; set; }

        public OrderByDirection Direction { get; set; }
    }
}