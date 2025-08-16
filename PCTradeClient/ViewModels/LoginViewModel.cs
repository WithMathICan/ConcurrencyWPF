using PCTradeClient.Services;
using System.Windows.Input;

namespace PCTradeClient.ViewModels
{
    public class LoginViewModel : ViewModelBase {
        private readonly IAuthenticationService _authService;
        private readonly MainViewModel _mainVm;

        private string _username = "";
        public string Username {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password = "";
        public string Password {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public string ErrorMessage { get; set; } = "";

        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthenticationService authService, MainViewModel mainVm) {
            _authService = authService;
            _mainVm = mainVm;
            LoginCommand = new RelayCommand(async o => await LoginAsync());
        }

        private async Task LoginAsync() {
            var success = await _authService.LoginAsync(Username, Password);
            if (success) {
                _mainVm.ShowMainApp();
            } else {
                ErrorMessage = "Invalid username or password";
            }
        }
    }

}
