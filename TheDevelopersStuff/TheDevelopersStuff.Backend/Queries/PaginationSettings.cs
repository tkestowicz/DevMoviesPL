using System;

namespace TheDevelopersStuff.Backend.Queries
{
    public class PaginationSettings
    {
        public PaginationSettings(int defaultPage = 1, int defaultPerPage = 10)
        {
            Page = defaultPage;
            PerPage = defaultPerPage;
        }

        internal int NumberOfRecords { get; set; }

        public int Page { get; set; }
        public int PerPage { get; set; }

        public int NumberOfPages
        {
            get {
                try
                {
                    return (int) Math.Ceiling((decimal) 50/PerPage);
                }
                catch (DivideByZeroException)
                {
                    return 0;
                }
            }
        }
    }
}