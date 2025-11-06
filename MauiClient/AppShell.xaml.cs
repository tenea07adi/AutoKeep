using MauiClient.UI.Pages;

namespace MauiClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(typeof(NewCarView).Name, typeof(NewCarView));
            Routing.RegisterRoute(typeof(CarView).Name, typeof(CarView));
            Routing.RegisterRoute(typeof(NewGenericReminderView).Name, typeof(NewGenericReminderView));
            Routing.RegisterRoute(typeof(GenericReminderView).Name, typeof(GenericReminderView));
            Routing.RegisterRoute(typeof(RescheduleReminderView).Name, typeof(RescheduleReminderView));
        }
    }
}
