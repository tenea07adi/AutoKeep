using MauiClient.UI.Abstract;
using MauiClient.ViewModels;

namespace MauiClient.UI.Pages;

public partial class NewGenericReminderView : BaseContentPage
{
	public NewGenericReminderView(NewGenericReminderViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}