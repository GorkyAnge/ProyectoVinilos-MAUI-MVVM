using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductoAppMAUI.Models;
using ProductoAppMAUI.Service;
using System.Windows.Input;

namespace ProductoAppMAUI.ViewModels
{
    public class DetalleProductoViewModel : ObservableRecipient
    {
        private readonly APIService _apiService;
        private Producto _producto;

        public string Nombre => _producto?.Nombre;
        public int Cantidad => _producto?.Stock ?? 0;
        public string Descripcion => _producto?.Descripcion;
        public string Imagen => _producto?.Imagen;

        public ICommand ResenaCommand => new AsyncRelayCommand(OnClickResena);
        public ICommand ResenaViewCommand => new AsyncRelayCommand(OnClickResenaView);
        public ICommand AddToCartCommand => new AsyncRelayCommand(OnClickAddCart);
            
        public DetalleProductoViewModel()
        {
            
        }
        public DetalleProductoViewModel(APIService apiService)
        {
            _apiService = apiService;
        }

        public async Task Initialize(Producto producto)
        {
            _producto = producto;
            OnPropertyChanged(nameof(Imagen));
            OnPropertyChanged(nameof(Nombre));
            OnPropertyChanged(nameof(Cantidad));
            OnPropertyChanged(nameof(Descripcion));
        }

        private async Task OnClickResena()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NuevaResenaPage(_apiService));
        }

        private async Task OnClickResenaView()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ResenaViewPage(_apiService));

        }

        private async Task OnClickAddCart()
        {
            string idUsuario = Preferences.Get("IdUser", "0");
            string idProducto = _producto.IdProducto.ToString();
            await _apiService.PostProductoEnCarrito(idUsuario, idProducto);
            await App.Current.MainPage.Navigation.PushAsync(new ProductoPage(_apiService));

        }
    }
}
