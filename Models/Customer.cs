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

        
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public bool IsActive { get; set; }

        public DateTime? UpdatedAt { get; set; }

         public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
