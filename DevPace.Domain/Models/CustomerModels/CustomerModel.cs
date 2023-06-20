namespace DevPace.Domain.Models.CustomerModels
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
