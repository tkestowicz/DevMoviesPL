namespace TheDevelopersStuff.Backend.Providers
{
    public interface IFiltersProvider<out TViewModel>
        where TViewModel : class 
    {
        TViewModel TakeAvailableFilters();
    }
}