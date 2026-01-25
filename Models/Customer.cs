using System.ComponentModel.DataAnnotations;

namespace TechSolutions.Web.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(100)]
        [Display(Name = "Surname")]
         public string SurName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        
      [DataType(DataType.Date)]
[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
public DateTime DateOfBirth { get; set; }


      [Required]
[Phone]
[StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits.")]
public string? Phone { get; set; }


        public bool IsActive { get; set; }

        public DateTime? UpdatedAt { get; set; }

         public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
