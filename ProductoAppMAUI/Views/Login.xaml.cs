using ProductoAppMAUI.Models;
using ProductoAppMAUI.ViewModels;
using ProductoAppMAUI.Service;

namespace ProductoAppMAUI;

public partial class Login : ContentPage
{
    private readonly APIService _APIService;
    private LoginViewModel _viewModel;

    public Login(APIService apiservice)
    {
        InitializeComponent();
        _APIService = apiservice;
        _viewModel = new LoginViewModel(_APIService);
        BindingContext = _viewModel;
    }

}