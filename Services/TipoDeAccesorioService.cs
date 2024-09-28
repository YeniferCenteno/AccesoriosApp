using AccesoriosApp.DTOs;
using System.Net.Http.Headers;

namespace AccesoriosApp.Services
{
    public class TipoDeAccesorioService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthServices _authServices;

        public TipoDeAccesorioService(HttpClient httpClient, AuthServices authServices)
        {
            _httpClient = httpClient;
            _authServices = authServices;
        }

        public async Task<List<TipoDeAccesorio>> GetTiposDeAccesorio()
        {
            try
            {
                var token = await _authServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<List<TipoDeAccesorio>>("api/tipoDeAccesorios/lista");

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al obtener accesorios. Revisar conexión a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener tipos de accesorios.");
            }
        }
    }
}
