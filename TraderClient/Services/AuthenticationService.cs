using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderClient.Services
{
    public class AuthenticationService {
        public string? Login(string username, string password) {
            if (username == "admin" && password == "123") {
                return "fake-jwt-token";
            }
            return null;
        }
    }
}
