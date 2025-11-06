
namespace MauiClient.Adapters.Popup
{
    public class PopupNotificationsService : IPopupNotificationsService
    {
        public async Task ShowPopupAsync(string title, string message, string cancelButtonText = "Cancel")
        {
            await Application.Current!.MainPage!.DisplayAlert(title, message, cancelButtonText);
        }

        public async Task<bool> ShowQuestionPopupAsync(string title, string message, string confirmationButtonMessage = "Yes", string refusalButtonText = "No")
        {
            return await Application.Current!.MainPage!.DisplayAlert(title, message, confirmationButtonMessage, refusalButtonText);
        }
    }
}
