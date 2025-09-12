using MauiClient.ViewModels;
using MauiClient.UI.Abstract;

namespace MauiClient.UI.Pages;

public partial class CarView : BaseContentPage
{
	public CarView(CarViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}