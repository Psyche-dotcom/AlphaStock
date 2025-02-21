namespace AlpaStock.Core.DTOs.Response.Auth
{
    public class LoginResultDto
    {
        public string Jwt { get; set; }
        public IList<string> UserRole { get; set; }
    }
}
