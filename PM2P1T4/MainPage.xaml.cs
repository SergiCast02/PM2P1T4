using PM2P1T4.models;
using PM2P1T4.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PM2P1T4
{
    public partial class MainPage : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile FileFoto = null;
        public MainPage()
        {
            InitializeComponent();
        }

        private Byte[] ConvertImageToByteArray()
        {
            if (FileFoto != null)
            {
                using (System.IO.MemoryStream memory = new MemoryStream())
                {
                    Stream stream = FileFoto.GetStream();
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }
            }
            return null;
        }

        private async void btnasqlite_Clicked(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                if (String.IsNullOrWhiteSpace(Nombre.Text)) { flag = true; }
                if (String.IsNullOrWhiteSpace(Descripcion.Text)) { flag = true; }
                if (ConvertImageToByteArray() == null) { flag = true; }
            }
            catch (Exception error)
            {
                await DisplayAlert("Aviso", ""+error, "OK");
            }
            
            
            if(!flag)
            {
                var person = new persona
                {
                    id = 0,
                    nombre = Nombre.Text,
                    descripcion = Descripcion.Text,
                    foto = ConvertImageToByteArray()
                };

                var result = await App.DBase.PersonSave(person);
                if (result > 0)
                {
                    await DisplayAlert("Aviso", "Persona adicionada con éxito", "OK");
                }
                else
                {
                    await DisplayAlert("Aviso", "Ha ocurrido un error", "OK");
                }

                bool answer = await DisplayAlert("Aviso", "¿Vaciar el formulario?", "Si", "No");
                //Debug.WriteLine("Answer: " + answer);
                if (answer)
                {
                    imgpersona.Source = null;
                    Nombre.Text = null;
                    Descripcion.Text = null;
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Debe llenar todos los campos", "OK");
            }
        }

        private async void tomarfoto_Clicked(object sender, EventArgs e)
        {
            FileFoto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Fotografias",
                Name = "foto.jpg",
                SaveToAlbum = true
            });

            // await DisplayAlert("Path directorio", FileFoto.Path, "OK");

            if (FileFoto != null)
            {
                txttomarfoto.Text = null;
                imgpersona.Source = ImageSource.FromStream(() =>
                {
                    return FileFoto.GetStream();
                });
            }
        }

        private async void verpersonas_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new listapersonas());
        }
    }
}
