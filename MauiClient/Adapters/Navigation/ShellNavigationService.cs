
namespace MauiClient.Adapters.Navigation
{
    public class ShellNavigationService : INavigationService
    {
        public async Task NavigateBackAsync(Dictionary<string, object>? navigationParams = null)
        {
            var dest = $"..";
            await NavigateToAsync(dest, navigationParams);
        }

        public async Task NavigateToDetailsPageAsync<T>(Dictionary<string, object>? navigationParams = null)
        {
            var dest = $"{typeof(T).Name}";
            await NavigateToAsync(dest, navigationParams);
        }

        public async Task NavigateToMainPageAsync<T>(Dictionary<string, object>? navigationParams = null)
        {
            var dest = $"//{typeof(T).Name}";
            await NavigateToAsync(dest, navigationParams);
        }
        
        public async Task NavigateToAsync(string route, Dictionary<string, object>? navigationParams = null)
        {
            ShellNavigationQueryParameters? parameters = null;

            if (navigationParams != null && navigationParams.Any())
            {
                parameters = new ShellNavigationQueryParameters(navigationParams);
            }

            if (parameters != null)
            {
                await Shell.Current.GoToAsync(route, parameters);
            }
            else
            {
                await Shell.Current.GoToAsync(route);
            }
        }
    }
}
