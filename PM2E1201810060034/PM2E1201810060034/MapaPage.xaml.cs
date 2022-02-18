using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms.Maps;
using Xamarin.Essentials;

namespace PM2E1201810060034
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Pin ubicacion = new Pin();
            ubicacion.Label = "San Pedro Sula";
            ubicacion.Address = "Cerca de UTH";
            ubicacion.Position = new Position(15.5510539, -88.0109923);
            mapas.Pins.Add(ubicacion);




            mapas.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(15.5510539, -88.0109923), Distance.FromKilometers(1)));





        }
    }

}
