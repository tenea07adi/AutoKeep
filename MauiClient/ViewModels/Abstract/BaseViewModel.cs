using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiClient.ViewModels.Abstract
{
    public abstract class BaseViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
        {
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            OnNavigatedToWithParams(query);
        }

        #region Lifecycle Methods
        public virtual void OnAppearing()
        {
            // Runs when the page appears
        }

        public virtual void OnNavigatedToWithParams(IDictionary<string, object> query)
        {
            // Runs when navigating to the page with parameters
        }

        public virtual void OnDisappearing()
        {
            // Runs when the page disappears
        }

        public virtual void OnNavigatedFrom()
        {
            // Runs when navigating away from the page
        }

        public virtual void OnBackButtonPressed()
        {
            // Runs when navigating back to the page
        }

        public virtual void OnNavigatedTo(NavigatedToEventArgs args)
        {
            // Runs when navigating back away from the page
        }
        #endregion

        protected void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
