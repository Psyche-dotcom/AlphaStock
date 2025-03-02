namespace AlpaStock.Core.DTOs.Response.Payment
{
    public class Paypal
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string Mode { get; set; }
        public string BaseUrl { get; set; }
        public string WebUrl { get; set; }
    }
}
