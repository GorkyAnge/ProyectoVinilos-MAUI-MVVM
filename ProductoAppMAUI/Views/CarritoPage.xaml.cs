using CommunityToolkit.Maui.Core;
using ProductoAppMAUI.Models;
using ProductoAppMAUI.ViewModels;
using ProductoAppMAUI.Service;
using System.Collections.ObjectModel;

namespace ProductoAppMAUI;

public partial class CarritoPage : ContentPage
{
    private readonly APIService _APIService;
    private CarritoViewModel _viewModel;
    public CarritoPage(APIService apiservice)
	{
		InitializeComponent();
        _APIService = apiservice;
        _viewModel = new CarritoViewModel(_APIService);
        BindingContext = _viewModel;
        _viewModel.LoadCarritoCommand.Execute(null);
    }

    private async void OnClickDetails(object sender, SelectedItemChangedEventArgs e)
    {
        
        Carrito carrito = e.SelectedItem as Carrito;
        ((CarritoViewModel)BindingContext).ProductDetailsCommand.Execute(carrito);

    }
    private async void TappedDeleteFromCart(object sender, TappedEventArgs e)
    {
        ((CarritoViewModel)BindingContext).DeleteFromCartCommand.Execute(null);
    }

}