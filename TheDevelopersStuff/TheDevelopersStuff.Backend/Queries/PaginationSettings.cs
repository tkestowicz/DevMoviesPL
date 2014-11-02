namespace TheDevelopersStuff.Backend.Queries
{
    public class PaginationSettings
    {
        public PaginationSettings(int defaultPage = 1, int defaultPerPage = 10)
        {
            Page = defaultPage;
            PerPage = defaultPerPage;
        }

        public int Page { get; set; }
        public int PerPage { get; set; }
    }
}