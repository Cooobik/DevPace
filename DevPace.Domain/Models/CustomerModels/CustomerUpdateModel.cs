using DevPace.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace DevPace.Domain.Models.CustomerModels
{
    public class CustomerUpdateModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxCompanyNameLength)]
        public string CompanyName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
