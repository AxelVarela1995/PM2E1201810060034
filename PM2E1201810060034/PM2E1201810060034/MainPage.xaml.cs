using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using PM2E1201810060034.Models;

namespace PM2E1201810060034
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        String base64Val = "";
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Datos_Ubicacion();
        }
        public async void Datos_Ubicacion()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS no esta activo", "Ok");
                    lbllatitud.Text = "00.0";
                    lbllongitud.Text = "00.0";
                }


                if (location != null)
                {

                    lbllatitud.Text = location.Latitude.ToString();
                    lbllongitud.Text = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {

                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location
            }
        }
        private void btnGuardar_Clicked(object sender, EventArgs e)
        {

            Int32 resultado = 0;
            var empleDB = new EmpleDB()
            {
                longitud = Convert.ToDouble(lbllongitud.Text),
                latitud = Convert.ToDouble(lbllatitud.Text),
                descripcion = this.txtDescripcion.Text,
                base64 = base64Val


            };


            if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                DisplayAlert("¡Hola!", "Los campos deben estar llenos", "OK");

            }
            else if (String.IsNullOrWhiteSpace(lbllatitud.Text))
            {
                DisplayAlert("¡Hola!", "Los campos deben estar llenos", "OK");

            }
            else if (String.IsNullOrWhiteSpace(lbllongitud.Text))
            {
                DisplayAlert("¡Hola!", "Los campos deben estar llenos", "OK");

            }


            else
            {

                using (SQLiteConnection conexion = new SQLiteConnection(App.ubicacionDB))
                {
                    conexion.CreateTable<EmpleDB>();

                    resultado = conexion.Insert(empleDB);

                    if (resultado > 0)
                        DisplayAlert("Hola", "Datos cargados satisfactoriamente", "OK");
                    else
                        DisplayAlert("Hola", "Error al cargar la información", "Ok");
                }

            }
        }


        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Locaciones());
        }

         private async void btncargarimg_Clicked(object sender, EventArgs e)
        {
            var tomarfoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "miApp",
                Name = "Image.jpg"

            });

            if (tomarfoto != null)
            {
                imagen.Source = ImageSource.FromStream(() => { return tomarfoto.GetStream(); });
            }

            Byte[] imagenByte = null;

            using (var stream = new MemoryStream())
            {
                tomarfoto.GetStream().CopyTo(stream);
                tomarfoto.Dispose();
                imagenByte = stream.ToArray();

                base64Val = Convert.ToBase64String(imagenByte);

            }

        }

    }
}
