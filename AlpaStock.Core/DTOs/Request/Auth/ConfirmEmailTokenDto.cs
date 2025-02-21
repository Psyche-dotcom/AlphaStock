namespace AlpaStock.Core.DTOs.Request.Auth
{
    public class ConfirmEmailTokenDto
    {
        public string email { get; set; }
        public int token { get; set; }
    }
}
