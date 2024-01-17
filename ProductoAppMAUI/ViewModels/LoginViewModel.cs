using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProductoAppMAUI.Models;
using ProductoAppMAUI.Service;
using System.Windows.Input;

namespace ProductoAppMAUI.ViewModels
{
    public partial class LoginViewModel : ObservableRecipient
    {
        private readonly APIService _apiService;

        private string _correo;
        private string _contrasenia;

        public string Correo
        {
            get => _correo;
            set => SetProperty(ref _correo, value);
        }

        public string Contrasenia
        {
            get => _contrasenia;
            set => SetProperty(ref _contrasenia, value);
        }

        public ICommand LoginCommand => new AsyncRelayCommand(LoginExecute);
        public ICommand RegisterCommand => new Command(RegisterExecute);
        public LoginViewModel()
        {
            
        }
        public LoginViewModel(APIService apiService)
        {
            _apiService = apiService;
        }

        private async Task LoginExecute()
        {
            if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(Contrasenia))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe completar todos los campos.", "OK");
                return;
            }

            User user = new User
            {
                Correo = Correo,
                Contrasenia = Contrasenia,
            };

            User loggedInUser = await _apiService.GetUser(user);
            if (loggedInUser != null)
            {
                Preferences.Set("username", loggedInUser.Nombre);
                Preferences.Set("IdUser", loggedInUser.IdUsuario.ToString());
                await App.Current.MainPage.Navigation.PushAsync(new ProductoPage(_apiService));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Usuario o Contraseña no encontrados, verifique que los ingresó correctamente", "OK");
            }
        }

        private void RegisterExecute()
        {
            App.Current.MainPage.Navigation.PushAsync(new Register(_apiService));
        }

    }
}
