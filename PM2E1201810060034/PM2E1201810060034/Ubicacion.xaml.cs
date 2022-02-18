using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using PM2E1201810060034.Models;

namespace PM2E1201810060034
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Locaciones : ContentPage
    {
        public Locaciones()
        {
            InitializeComponent();
        }


        EmpleDB itemLocacion = new EmpleDB();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SQLiteConnection conexion = new SQLiteConnection(App.ubicacionDB);
            conexion.CreateTable<EmpleDB>();
            var ubicacion = conexion.Table<EmpleDB>().ToList();
            ListaUbicaciones.ItemsSource = ubicacion;
            conexion.Close();
        }

        private void listviewSelect(object sender, ItemTappedEventArgs e)
        {
            itemLocacion = (EmpleDB)e.Item;


        }
        private async void btnBorrar_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection conexion = new SQLiteConnection(App.ubicacionDB);
            conexion.Table<EmpleDB>().Delete(x => x.id == itemLocacion.id);


            conexion.CreateTable<EmpleDB>();
            var ubicacion = conexion.Table<EmpleDB>().ToList();
            ListaUbicaciones.ItemsSource = ubicacion;
            conexion.Close();
        }

        private async void btnMapas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }

       
    }
}