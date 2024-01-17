using ProductoAppMAUI.Models;
using ProductoAppMAUI.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductoAppMAUI;

public partial class NuevaResenaPage : ContentPage
{
    private Producto _producto;
    private readonly APIService _APIService;
    public NuevaResenaPage(APIService apiservice)
    {
        InitializeComponent();
        _APIService = apiservice;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _producto = BindingContext as Producto;
        IdViniloEntry.Text = _producto.IdProducto.ToString();
        string name = Preferences.Get("username", "0");
        UsuarioEntry.Text = name;
    }

    private async void OnClickEnviarResena(object sender, EventArgs e)
    {
        //Verificar que el formulario no est� vac�o
        if (string.IsNullOrEmpty(TextoEntry.Text))
        {
            await DisplayAlert("Error", "Debe completar todos los campos.", "OK");
            return;
        }

        Resena resena = new Resena()
        {
            ViniloId = Convert.ToInt32(IdViniloEntry.Text),
            Usuario = UsuarioEntry.Text,
            Texto = TextoEntry.Text
        };

        await _APIService.PostResena(resena);
        await DisplayAlert("Rese�a Agregada", "Su rese�a se public� correctamente.", "OK");
        await Navigation.PopAsync();
    }
}