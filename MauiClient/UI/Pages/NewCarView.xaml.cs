using MauiClient.ViewModels;
using MauiClient.UI.Abstract;

namespace MauiClient.UI.Pages;

public partial class NewCarView : BaseContentPage
{
	public NewCarView(NewCarViewModel viewModel) : base(viewModel)
	{
        InitializeComponent();
    }
}