using MauiClient.ViewModels;
using MauiClient.UI.Abstract;

namespace MauiClient.UI.Pages;

public partial class CarsListView : BaseContentPage
{
	public CarsListView(CarsListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}