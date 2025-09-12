
namespace MauiClient.Adapters.Navigation
{
    public interface INavigationService
    {
        public Task NavigateBackAsync(Dictionary<string, object>? navigationParams = null);
        public Task NavigateToDetailsPageAsync<T>(Dictionary<string, object>? navigationParams = null);
        public Task NavigateToMainPageAsync<T>(Dictionary<string, object>? navigationParams = null);
        public Task NavigateToAsync(string route, Dictionary<string, object>? navigationParams = null);
    }
}
