using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ProductoAppMAUI.Models;
using ProductoAppMAUI.Service;

namespace ProductoAppMAUI.ViewModels
{
    public partial class CarritoViewModel : ObservableRecipient
    {
        private readonly APIService _apiService;

        public ObservableCollection<Carrito> CarritoItems { get; private set; }

        [ObservableProperty]
        public ObservableCollection<Carrito> listaCarrito;

        public ICommand ComprarCommand => new AsyncRelayCommand(ComprarExecute);
        public ICommand DeleteFromCartCommand => new AsyncRelayCommand(DeleteFromCartExecute);
        public ICommand ProductDetailsCommand => new Command<Carrito>(async (carrito) => await ProductDetailsExecute(carrito));

        public ICommand LoadCarritoCommand => new AsyncRelayCommand(LoadCarritoItems);

        public CarritoViewModel()
        {
            
        }
        public CarritoViewModel(APIService apiService)
        {
            _apiService = apiService;
            CarritoItems = new ObservableCollection<Carrito>();
        }

        public async Task LoadCarritoItems()
        {
            string IdUsuario = Preferences.Get("IdUser", "0");
            Console.WriteLine($"IdUsuario: {IdUsuario}");
            List<Carrito> listaCarrito = await _apiService.GetProductosCarrito(IdUsuario);
            
            CarritoItems = new ObservableCollection<Carrito>(listaCarrito);
            OnPropertyChanged(nameof(CarritoItems));
        }


        private async Task ComprarExecute()
        {
            string IdUsuario = Preferences.Get("IdUser", "0");
            List<Carrito> carrito = await _apiService.GetProductosCarrito(IdUsuario);
            if (carrito.Count == 0)
            {
                await App.Current.MainPage.Navigation.PushAsync(new ProductoPage(_apiService));
            }
            else
            {
                await _apiService.DeleteCarrito(IdUsuario);
                await App.Current.MainPage.Navigation.PushAsync(new ProductoPage(_apiService));
            }
        }

        private async Task DeleteFromCartExecute()
        {
            if (Preferences.Get("ProductCarritoId", "0").Equals("0"))
            {
                await App.Current.MainPage.Navigation.PushAsync(new ProductoPage(_apiService));
            }
            else if (await _apiService.DeleteProductoCarrito(Preferences.Get("ProductCarritoId", "0")) == true)
            {
                Preferences.Set("ProductCarritoId", "0");
                await App.Current.MainPage.Navigation.PushAsync(new ProductoPage(_apiService));
            }
        }

        private async Task ProductDetailsExecute(Carrito carrito)
        {
            Preferences.Set("ProductCarritoId", carrito.IdProductoEnCarrito.ToString());
        }
    }
}
