using Microsoft.Maui.Controls;
using System.IO;

namespace LocationTracker
{
    public partial class App : Application
    {
        static LocationDatabase database;

        public App()
        {
            InitializeComponent();
            MainPage = new MainView();
        }

        public static LocationDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new LocationDatabase(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "locations.db3"));
                }
                return database;
            }
        }
    }
}
