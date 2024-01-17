using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using ProductoAppMAUI.Models;
using ProductoAppMAUI.ViewModels;
using ProductoAppMAUI.Service;
using System.Collections.ObjectModel;

namespace ProductoAppMAUI;

public partial class ProductoPage : ContentPage
{
    private readonly APIService _APIService;
    private ProductoViewModel _viewModel;
    public ProductoPage(APIService apiService)
    {
        InitializeComponent();
        _APIService = apiService;
        _viewModel = new ProductoViewModel(_APIService);
        BindingContext = _viewModel;
        _viewModel.LoadProductosCommand.Execute(null);
    }

    private void OnClickLogout(object sender, EventArgs e)
    {
        ((ProductoViewModel)BindingContext).LogoutCommand.Execute(null);
    }

    private void OnClickShowDetails(object sender, SelectedItemChangedEventArgs e)
    {
        Producto producto = e.SelectedItem as Producto;
        ((ProductoViewModel)BindingContext).OpenDetailsCommand.Execute(producto);
    }
}

