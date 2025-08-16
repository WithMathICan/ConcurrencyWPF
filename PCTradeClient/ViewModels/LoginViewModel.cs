using PCTradeClient.Services;
using System.Windows.Input;

namespace PCTradeClient.ViewModels
{
    public class LoginViewModel : ViewModelBase {
        private readonly IAuthenticationService _authService;
        private readonly MainViewModel _mainVm;

        public string Username { get; set; } = "";
        public string ErrorMessage { get; set; } = "";

        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthenticationService authService, MainViewModel mainVm) {
            _authService = authService;
            _mainVm = mainVm;
            LoginCommand = new RelayCommand(async (p) => await LoginAsync((string)p));
        }

        private async Task LoginAsync(string password) {
            var success = await _authService.LoginAsync(Username, password);
            if (success) {
                _mainVm.ShowMainApp();
            } else {
                ErrorMessage = "Invalid username or password";
            }
        }
    }

}
