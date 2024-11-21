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

        // CRear tipo de accesorio
        public async Task<Boolean> CreateTipodeAccesorio(TipoDeAccesorioDTO tipoDeAccesorioDTO)
        {
            try
            {
                var token = await _authServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync("api/tipoDeAccesorios", tipoDeAccesorioDTO);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error al crear tipo de accesorio. Revisar conexión a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error inesperado al crear tipo de accesorio.");
            }
        }
        //Mostrar tipo de accesorio
        public async Task<ContentResponseTipoAccesorio> GetTiposDeAccesorio(int page = 0, int size = 5, string nombre = "")
        {
            try
            {
                var token = await _authServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var respons = await _httpClient.GetFromJsonAsync<ContentResponseTipoAccesorio>($"api/tipoDeAccesorios?page={page}&size={size}&nombre={nombre}");

                return respons;
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

        // Método para editar un tipo de accesorio
        public async Task<bool> EditTipoDeAccesorio(int tipoDeAccesorioId, TipoDeAccesorio tipoDeAccesorio)
        {
            try
            {
                var token = await _authServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o inválido. Iniciar sesión");
                }

                // Crear el payload con los datos de tipo de accesorio a editar
                var payload = new TipoDeAccesorioEditar()
                {
                    Id = tipoDeAccesorio.Id,
                    Nombre = tipoDeAccesorio.Nombre
                };

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PutAsJsonAsync($"api/tipoDeAccesorios/{tipoDeAccesorioId}", payload);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error al editar el tipo de accesorio. Revisar conexión a internet.");
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error inesperado al editar el tipo de accesorio.");
            }
        }

        // Obtener un tipo de accesorio por su ID (Nuevo método) va con iditar tipo de accesorio 
        public async Task<TipoDeAccesorio> GetTipoDeAccesorioById(int tipoDeAccesorioId)
        {
            try
            {
                var token = await _authServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetFromJsonAsync<TipoDeAccesorio>($"api/tipoDeAccesorios/{tipoDeAccesorioId}");

                return response;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error al obtener el tipo de accesorio. Revisar conexión a internet.");
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error inesperado al obtener el tipo de accesorio.");
            }
        }

        public async Task<bool> DeleteTipoDeAccesorio(int tipodeaccesorioId)
        {
            try
            {
                var token = await _authServices.GetToken();
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidOperationException("El token es nulo o invalido. Iniciar sesión");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.DeleteAsync($"api/tipoDeAccesorios/{tipodeaccesorioId}");

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Error al eliminar el tipo de accesorio. Revisar conexión a internet.");
            }
            catch (Exception)
            {
                throw new Exception("Ha ocurrido un error inesperado al eliminar el tipo de accesorio.");
            }
        }
    }
}
