using PCTradeClient.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace PCTradeClient.Services
{
    public interface IAuthenticationService {
        string? JwtToken { get; }
        Task<bool> LoginAsync(string username, string password);
        void Logout();
        bool IsAuthenticated { get; }
    }

    public class AuthenticationService(HttpClient httpClient) : IAuthenticationService {
        private readonly HttpClient _httpClient = httpClient;
        public string? JwtToken { get; private set; }

        public bool IsAuthenticated => !string.IsNullOrEmpty(JwtToken);

        public async Task<bool> LoginAsync(string username, string password) {
            var request = new LoginRequest { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("https://yourserver/api/auth/login", request);
            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                JwtToken = result?.JwtToken;
                return true;
            }
            return false;
        }

        public void Logout() {
            JwtToken = null;
        }
    }

}
