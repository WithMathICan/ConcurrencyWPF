using System.Net.Http;

namespace TraderClient.Services
{
    public static class HttpClientFactory {
        private static HttpClient? _client;

        public static HttpClient GetClient() {
            if (_client == null) {
                var handler = new HttpClientHandler();
                _client = new HttpClient(handler) {
                    BaseAddress = new Uri("https://yourserver/api/")
                };
            }

            return _client;
        }
    }

}
