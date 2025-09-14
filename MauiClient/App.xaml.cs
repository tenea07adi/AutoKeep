using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DataBase;

namespace MauiClient
{
    public partial class App : Application
    {
        public App(DataBaseContext dataBase)
        {
            InitializeComponent();

            dataBase.Database.Migrate();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}