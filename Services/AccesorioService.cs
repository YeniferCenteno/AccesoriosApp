using AccesoriosApp.DTOs;
using System.Net.Http.Headers;

namespace AccesoriosApp.Services
{
    public class AccesorioService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthServices _authServices;

        public AccesorioService(HttpClient httpClient, AuthServices authServices)
        {
            _httpClient = httpClient;
            _authServices = authServices;
        }

    public async Task<ContentResponse> GetAccesorios(){

        try
        {
            var token = await _authServices.GetToken();
            if(string.IsNullOrEmpty(token))
            {
                throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
            }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<ContentResponse>("api/accesorios");

                return response;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Error al obtener accesorios. Revisar conexión a internet.");
        }
            catch(Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener accesorios.");
            }
        }

        //Método para crear nuevo accesorio
    public async Task<Boolean> CreateAccecorio(MultipartFormDataContent formDataContent)
        {
            try
            {
                var token = await _authServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsync("api/accesorios", formDataContent);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al crear accesorios. Revisar conexión a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al crear accesorios.");
            }
        }
        // Método para eliminar un accesorio
        public async Task<bool> DeleteAccesorio(int accesorioId)
        {
            try
            {
                var token = await _authServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync($"api/accesorios/{accesorioId}");

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error al eliminar el accesorio. Revisar conexión a internet.");
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error inesperado al eliminar el accesorio.");
            }
        }
    }
}
