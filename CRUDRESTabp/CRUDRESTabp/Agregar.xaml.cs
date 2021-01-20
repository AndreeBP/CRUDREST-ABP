using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CRUDRESTabp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Agregar : ContentPage
    {
        
        private const string Urladd = "https://192.168.0.2:3050/clientes/add";

        private readonly HttpClient _client = new HttpClient();

        public Agregar(bool isNew = false)
        {
            InitializeComponent();
        }
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {

            if (await validarFormulario())
            {
                Cliente product = new Cliente { Nombre = nombre.Text, Descripcion = descripcion.Text,};
                string content = JsonConvert.SerializeObject(product);
                await _client.PostAsync(Urladd, new StringContent(content, Encoding.UTF8, "application/json"));

                await DisplayAlert("Exito", "Cliente Registrado correctamente", "OK");

                await Navigation.PopAsync();


            }

        }

        private async Task<bool> validarFormulario()
        {

            if (String.IsNullOrWhiteSpace(nombre.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo del nombre es obligatorio.", "OK");
                return false;
            }
            else
            {
                //Valida si se ingresan caracteres especiales
                string caractEspecial = @"^[^ ][a-zA-Z ]+[^ ]$";
                bool resultado = Regex.IsMatch(nombre.Text, caractEspecial, RegexOptions.IgnoreCase);
                if (!resultado)
                {
                    await this.DisplayAlert("Advertencia", "No se aceptan caracteres especiales, intente de nuevo.", "OK");
                    return false;
                }
            }

            if (String.IsNullOrWhiteSpace(descripcion.Text))
            {
                await this.DisplayAlert("Advertencia", "El campo del descripcion es obligatorio.", "OK");
                return false;
            }
            else
            {
                //Valida si se ingresan caracteres especiales
                string caractEspecial = @"^[^ ][a-zA-Z ]+[^ ]$";
                bool resultado = Regex.IsMatch(descripcion.Text, caractEspecial, RegexOptions.IgnoreCase);
                if (!resultado)
                {
                    await this.DisplayAlert("Advertencia", "No se aceptan caracteres especiales, intente de nuevo.", "OK");
                    return false;
                }
            }

            return true;

        }


        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }



    }
}