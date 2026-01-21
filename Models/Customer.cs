using System.ComponentModel.DataAnnotations;

namespace TechSolutions.Web.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(100)]
        public string SurName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Phone]
        public string? Phone { get; set; }

        public bool IsActive { get; set; }
    }
}
