
namespace MauiClient.Adapters.Popup
{
    public interface IPopupNotificationsService
    {
        public Task ShowPopupAsync(string title, string message, string cancelButtonText = "Cancel");
        public Task<bool> ShowQuestionPopupAsync(
            string title, 
            string message, 
            string confirmationButtonMessage = "Yes", 
            string refusalButtonText = "No");
    }
}
