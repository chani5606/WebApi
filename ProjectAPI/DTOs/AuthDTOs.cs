namespace ProjectAPI.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
      
    }
}
