namespace E_Learning.Areas.Payment.Models
{
    public class PayPalClient
    {
        public string Mode { get;  }
        
        public string ClientId { get; }
        
        public string ClientSecret { get; }

        public string Baseurl 
            => Mode == "Live" ? "https://api-m.paypal.com" : "https://api-m.sandbox.paypal.com";

        public PayPalClient(string mode, string clientId, string clientSecret)
        {
            Mode = mode;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}
