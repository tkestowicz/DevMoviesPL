using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheDevelopersStuff.Backend.DataSources.DTO;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.Providers
{
    public interface IVideoProvider
    {
        Task<List<ChannelDTO>> ChannelsData();
    }
}