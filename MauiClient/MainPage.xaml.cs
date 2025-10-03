namespace MauiClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void LearnMore_Clicked(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new Uri("https://www.autokeep.aditenea.net");
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., if the browser is not available or the URL is invalid
                Console.WriteLine($"Error opening browser: {ex.Message}");
            }
        }
    }

}
