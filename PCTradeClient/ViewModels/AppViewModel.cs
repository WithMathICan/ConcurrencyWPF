using PCTradeClient.Services;
using System.Windows.Input;

namespace PCTradeClient.ViewModels {
    public class AppViewModel : ViewModelBase {
        private readonly IAuthenticationService _authService;
        private readonly MainViewModel _mainVm;

        public string WelcomeMessage => $"Welcome! Your token is: {_authService.JwtToken}";

        public ICommand LogoutCommand { get; }

        public AppViewModel(IAuthenticationService authService, MainViewModel mainVm) {
            _authService = authService;
            _mainVm = mainVm;

            LogoutCommand = new RelayCommand(_ => Logout());
        }

        private void Logout() {
            _authService.Logout();
            _mainVm.ShowLogin(); // switch back to login screen
        }
    }
}
