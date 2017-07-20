using System.ComponentModel.DataAnnotations;

namespace Checkout.Server.Host.OAuth
{
    public class AudienceModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}