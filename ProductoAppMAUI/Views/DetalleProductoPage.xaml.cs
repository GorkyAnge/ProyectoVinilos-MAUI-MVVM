using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using ProductoAppMAUI.Models;
using ProductoAppMAUI.ViewModels;
using ProductoAppMAUI.Service;
using System.Collections.ObjectModel;

namespace ProductoAppMAUI;

public partial class DetalleProductoPage : ContentPage
{

    private Producto _producto;
    private readonly APIService _APIService;
    private DetalleProductoViewModel _viewModel;

    public DetalleProductoPage(APIService apiservice, Producto product)
    {
        InitializeComponent();
        _APIService = apiservice;
        _viewModel = new DetalleProductoViewModel(_APIService);
        BindingContext = _viewModel;
        _viewModel.Initialize(product);
    }

}