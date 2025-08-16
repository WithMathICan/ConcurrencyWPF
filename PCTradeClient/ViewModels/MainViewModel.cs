using PCTradeClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCTradeClient.ViewModels
{
    public class MainViewModel : ViewModelBase {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel {
            get => _currentViewModel;
            set {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private readonly IAuthenticationService _authService;

        public MainViewModel(IAuthenticationService authService) {
            _authService = authService;
            if (_authService.IsAuthenticated)
                ShowMainApp();
            else
                ShowLogin();
        }

        public void ShowMainApp() {
            CurrentViewModel = new AppViewModel(_authService, this);
        }

        public void ShowLogin() {
            CurrentViewModel = new LoginViewModel(_authService, this);
        }
    }

}
