using MauiClient.UI.Abstract;
using MauiClient.ViewModels;

namespace MauiClient.UI.Pages;

public partial class GenericReminderView : BaseContentPage
{
	public GenericReminderView(GenericReminderViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}