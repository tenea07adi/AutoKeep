using MauiClient.UI.Abstract;
using MauiClient.ViewModels;

namespace MauiClient.UI.Pages;

public partial class RescheduleReminderView : BaseContentPage
{
	public RescheduleReminderView(RescheduleReminderViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}