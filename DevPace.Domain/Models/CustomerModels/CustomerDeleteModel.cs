using System.ComponentModel.DataAnnotations;

namespace DevPace.Domain.Models.CustomerModels
{
    public class CustomerDeleteModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
