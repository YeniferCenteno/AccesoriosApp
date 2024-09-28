using AccesoriosApp.DTOs;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;

namespace AccesoriosApp.Services
{
    public class AuthServices
    {
            private readonly ProtectedLocalStorage _localStorage;
            private readonly HttpClient _httpClient;
            private string _token;

            public AuthServices(ProtectedLocalStorage localStorage, HttpClient httpClient)
            {
                _localStorage = localStorage;
                _httpClient = httpClient;
            }

            public async Task<LoginResponse> Login(AuthDTO payload)
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", payload);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    return result;
                }

            return new LoginResponse() { token = "" };
            }
            public async Task SetToken(string token)
            {
                _token = token;
                await _localStorage.SetAsync("token", token);
            }
            public async Task<string> GetToken()
            {
                var result = await _localStorage.GetAsync<string>("token");
                if (string.IsNullOrEmpty(_token))
                {

                    if (!result.Success || string.IsNullOrEmpty(result.Value))
                    {
                        _token = null;
                        return null;
                    }
                    _token = result.Value;
                    return result.Value;
                }
                return _token;
            }
            public async Task<bool> IsAuthenticated()
            {
                var token = await GetToken();
                return !string.IsNullOrEmpty(token) && !IsExpiredToken(token);
            }
            public bool IsExpiredToken(string token)
            {
                var jwtToken = new JwtSecurityToken(token);
                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            public async Task Logout()
            {
                _token = null;
                await _localStorage.DeleteAsync("token");
            }
        
    }
}
