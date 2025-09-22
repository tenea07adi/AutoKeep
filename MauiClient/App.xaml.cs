using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;

namespace MauiClient
{
    public partial class App : Application
    {
        public App(DataBaseContext dataBase)
        {
            InitializeComponent();

            dataBase.Database.Migrate();

            Application.Current.UserAppTheme = AppTheme.Light; // Force light theme
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}