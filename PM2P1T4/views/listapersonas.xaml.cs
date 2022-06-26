using PM2P1T4.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2P1T4.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class listapersonas : ContentPage
    {
        public listapersonas()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ListaPersonas.ItemsSource = await App.DBase.obtenerListaPersonas();
        }

        private async void ListaPersonas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var person = (persona)e.Item;
            var secondPage = new mostrarfoto();
            secondPage.BindingContext = person;
            await Navigation.PushAsync(secondPage);
        }
    }
}