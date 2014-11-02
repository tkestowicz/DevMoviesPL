using System.Collections.Generic;
using System.Threading.Tasks;
using TheDevelopersStuff.Backend.DataSources.DTO;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.DataSources
{
    public interface IVideosDataSource
    {
        Task<List<ChannelDTO>> FindAll();
    }
}