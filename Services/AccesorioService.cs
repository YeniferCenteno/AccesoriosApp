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

        // Método para obtener un accesorio por ID
        public async Task<Accesorio> GetAccesorioById(int accesorioId)
        {
            try
            {
                var token = await _authServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o inválido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var accesorio = await _httpClient.GetFromJsonAsync<Accesorio>($"api/accesorios/{accesorioId}");

                return accesorio;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error al obtener el accesorio. Revisar conexión a internet.");
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener el accesorio.");
            }
        }


        // Método para editar un accesorio
        public async Task<bool> EditAccesorio(int accesorioId, Accesorio accesorio)
        {
            try
            {
                var token = await _authServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o inválido. Iniciar sesión");
                }
                AccesorioEditar payload = new AccesorioEditar()
                {
                    id = accesorio.Id,
                    nombre = accesorio.Nombre,
                    descripcion = accesorio.Descripcion,
                    tipoDeAccesorioId = accesorio.TipoDeAccesorio.Id,
                    urlFoto = accesorio.UrlFoto
                };

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Console.WriteLine(accesorio.TipoDeAccesorio.Id);
                var response = await _httpClient.PutAsJsonAsync($"api/accesorios/{accesorioId}", payload);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error al editar el accesorio. Revisar conexión a internet.");
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error inesperado al editar el accesorio.");
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
