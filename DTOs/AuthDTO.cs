namespace AccesoriosApp.DTOs
{
    public class AuthDTO
    {
        public string login { get; set; } = null!;
        public string clave { get; set; } = null!;
    }
    public class LoginResponse
    {
        public string token { get; set; } = null!;
    }
}
