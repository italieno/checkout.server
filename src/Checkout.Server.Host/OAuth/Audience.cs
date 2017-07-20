using System.ComponentModel.DataAnnotations;

namespace Checkout.Server.Host.OAuth
{
    public class Audience
    {
        public string ClientId { get; set; }
        public string Base64Secret { get; set; }
        public string Name { get; set; }
        public bool IsConfidential { get; set; }
    }
}