using MauiClient.ViewModels.Abstract;

namespace MauiClient.UI.Abstract;

public class BaseContentPage : ContentPage
{
    protected readonly BaseViewModel _viewModel;

    public BaseContentPage(BaseViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    #region Lifecycle Methods
    // These methods are called during the page's lifecycle events
    // and delegate the calls to the ViewModel if it is of type BaseViewModel.
    // This ensures that the ViewModel can respond to these events appropriately
    // and maintain a clean separation of concerns between the View and ViewModel.

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is BaseViewModel vm)
            vm.OnAppearing();
    }

    protected override void OnDisappearing() {
        base.OnDisappearing();

        if (BindingContext is BaseViewModel vm)
            vm.OnDisappearing();
    }

    protected override bool OnBackButtonPressed()
    {
        if (BindingContext is BaseViewModel vm)
            vm.OnBackButtonPressed();

        return base.OnBackButtonPressed();
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

        if (BindingContext is BaseViewModel vm)
            vm.OnNavigatedFrom();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (BindingContext is BaseViewModel vm)
            vm.OnNavigatedTo(args);
    }
    #endregion
}