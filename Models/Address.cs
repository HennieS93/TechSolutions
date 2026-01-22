using System.ComponentModel.DataAnnotations;

namespace TechSolutions.Web.Models
{
    public class Address
    {
        public int ID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public string AddressLine1 { get; set; } = string.Empty;

        public string? AddressLine2 { get; set; }

        [Required]
        public string City { get; set; } = string.Empty;

                [Required]
        public string Country { get; set; } = string.Empty;

        [Required]
        public string PostalCode { get; set; } = string.Empty;

        // Navigation
        public Customer? Customer { get; set; } = null!;
    }
}
