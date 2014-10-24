using System.Collections.Generic;
using TheDevelopersStuff.Backend.Queries;
using TheDevelopersStuff.Backend.ViewModels;

namespace TheDevelopersStuff.Backend.DataSources
{
    public interface IVideosDataSource
    {
        List<VideosViewModel> FindAll(FindVideosQuery query);
    }
}