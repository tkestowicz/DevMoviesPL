using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Queries
{
    public class OrderSettings
    {
        private string propertyName;

        public string PropertyName
        {
            get { return propertyName; }
            set
            {
                if(string.IsNullOrWhiteSpace(value) == false)
                    propertyName = value;
            }
        }

        public OrderDirectionEnum Direction { get; set; }
    }
}