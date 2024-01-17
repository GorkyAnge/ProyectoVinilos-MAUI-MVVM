using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductoAppMAUI.Models;
using ProductoAppMAUI.Service;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductoAppMAUI.ViewModels
{
    public partial class ProductoViewModel : ObservableRecipient
    {
        private readonly APIService _apiService;

        public ObservableCollection<Producto> Productos { get; private set; }
        public string Username => Preferences.Get("username", "0");

        [ObservableProperty]
        public ObservableCollection<Producto> listaProductos;


        public ICommand OpenDetailsCommand => new Command<Producto>(async (producto) => await OpenDetailsAsync(producto));
        public ICommand LogoutCommand => new AsyncRelayCommand(LogoutExecute);
        public ICommand LoadProductosCommand => new AsyncRelayCommand(LoadProductos);
        public ICommand OpenCarrito => new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new CarritoPage(_apiService)));


        public ProductoViewModel()
        {
            
        }
        public ProductoViewModel(APIService apiService)
        {
            _apiService = apiService;
            Productos = new ObservableCollection<Producto>();
          
        }

        private async Task OpenDetailsAsync(Producto producto)
        {
            await App.Current.MainPage.Navigation.PushAsync(new DetalleProductoPage(_apiService, producto));
        }
        private async Task LoadProductos()
        {
            List<Producto> listaProductos = await _apiService.GetProductos();
            Productos = new ObservableCollection<Producto>(listaProductos);
            OnPropertyChanged(nameof(Productos));
        }

        private async Task LogoutExecute()
        {
            Preferences.Set("username", "0");
            await App.Current.MainPage.Navigation.PopAsync();
            //await Shell.Current.GoToAsync("///HomePage");
        }
    }
}
