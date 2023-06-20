using DevPace.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace DevPace.Domain.Models.CustomerModels
{
    public class CustomerCreateModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.MaxCompanyNameLength)]
        public string CompanyName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
