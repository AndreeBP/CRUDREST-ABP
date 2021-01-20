using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace CRUDRESTabp
{
    public partial class MainPage : ContentPage
    {

        private const string Url = "https://192.168.0.2:3050/clientes";
        private const string Urladd = "https://192.168.0.2:3050/clientes/add";

        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Cliente> _products;

        public MainPage()
        {
            InitializeComponent();

        }
        protected override async void OnAppearing()
        {
            string content = await _client.GetStringAsync(Url);
            List<Cliente> products = JsonConvert.DeserializeObject<List<Cliente>>(content); //Deserializar json
            _products = new ObservableCollection<Cliente>(products);
            MyListView.ItemsSource = _products; //llenar lista
            base.OnAppearing();
        }


        //para agregar
        async void OnAddItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Agregar());
        }


        //Parallel edicion
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditaryEliminar
            {
                BindingContext = e.SelectedItem as Cliente
            });
        }

    }
}

