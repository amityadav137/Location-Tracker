using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Maps;

namespace LocationTracker
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            DisplaySavedPath();
        }

        private async void OnTrackClicked(object sender, EventArgs e)
        {
            var location = await Geolocation.Default.GetLocationAsync();
            if (location != null)
            {
                var entry = new LocationEntry
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    Timestamp = DateTime.Now
                };
                await App.Database.SaveLocationAsync(entry);
                await DisplaySavedPath();
            }
        }

        private async Task DisplaySavedPath()
        {
            var locations = await App.Database.GetLocationsAsync();
            map.Pins.Clear();
            map.MapElements.Clear();

            if (locations.Count > 0)
            {
                var polyline = new Polyline
                {
                    StrokeColor = Colors.Red,
                    StrokeWidth = 5
                };
                foreach (var loc in locations)
                {
                    polyline.Geopath.Add(new Microsoft.Maui.Devices.Sensors.Location(loc.Latitude, loc.Longitude));
                }
                map.MapElements.Add(polyline);

                foreach (var loc in locations)
                {
                    var pin = new Pin
                    {
                        Location = new Microsoft.Maui.Devices.Sensors.Location(loc.Latitude, loc.Longitude),
                        Label = $"Visited at {loc.Timestamp}"
                    };
                    map.Pins.Add(pin);
                }

                var lastLoc = locations.Last();
                map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Microsoft.Maui.Devices.Sensors.Location(lastLoc.Latitude, lastLoc.Longitude),
                    Distance.FromKilometers(2)));
            }
        }
    }
}
