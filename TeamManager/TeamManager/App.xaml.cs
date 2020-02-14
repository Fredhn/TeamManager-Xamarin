using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamManager.Services;
using TeamManager.Views;
using TeamManager.Data;
using System.IO;

namespace TeamManager
{
    public partial class App : Application
    {
        static TeamManagerDatabase _database;

        public static TeamManagerDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new TeamManagerDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TeamManager.db3"));
                }
                return _database;
            }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
