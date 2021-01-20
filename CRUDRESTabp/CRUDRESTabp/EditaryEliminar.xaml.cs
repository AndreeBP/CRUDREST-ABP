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
    public partial class EditaryEliminar : ContentPage
    {

        private const string UrlDelete = "https://192.168.0.2:3050/clientes/delete"; // Eliminar cliente
        private const string UrlUpdate = "https://192.168.0.2:3050/clientes/update/{id}"; // Actualizar cliente

        private readonly HttpClient _client = new HttpClient();


        public EditaryEliminar(bool isNew = false)
        {
            InitializeComponent();
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {



            if (await validarFormulario())
            {
                var newId = Convert.ToInt32(id.Text);

                Cliente product = new Cliente { Id = newId, Nombre = nombre.Text, Descripcion = descripcion.Text};

                string content = JsonConvert.SerializeObject(product);
                await _client.PutAsync(UrlUpdate, new StringContent(content, Encoding.UTF8, "application/json"));


                await DisplayAlert("Exito", "Cliente Actualizado correctamente", "OK");

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

                string caractEspecial = "123456789";
                bool resultado = Regex.IsMatch(nombre.Text, caractEspecial, RegexOptions.IgnoreCase);
                if (!resultado)
                {
                    await this.DisplayAlert("Advertencia", "No se acpetan numeros", "OK");
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

                string caractEspecial = "123456789";
                bool resultado = Regex.IsMatch(descripcion.Text, caractEspecial, RegexOptions.IgnoreCase);
                if (!resultado)
                {
                    await this.DisplayAlert("Advertencia", "No se acpetan numeros", "OK");
                    return false;
                }
            }


            return true;

        }



        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var newId = Convert.ToInt32(id.Text);

            await _client.DeleteAsync(UrlDelete + newId);

            await DisplayAlert("Operación Exitosa", $"Cliente: {this.nombre.Text} Eliminado", "OK");

            await Navigation.PopAsync();
        }





        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


    }

}