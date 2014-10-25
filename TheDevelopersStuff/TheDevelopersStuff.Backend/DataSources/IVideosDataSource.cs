using System.Collections.Generic;
using System.Threading.Tasks;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.DataSources
{
    public interface IVideosDataSource
    {
        Task<List<ConferenceViewModel>> FindAll();
    }
}