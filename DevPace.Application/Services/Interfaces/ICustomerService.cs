using DevPace.Domain.Models.CustomerModels;

namespace DevPace.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerModel> CreateCustomer(CustomerCreateModel customerCreateModel);
        Task<CustomerModel?> GetCustomer(Guid id);
        Task<IEnumerable<CustomerModel>> GetCustomers(CustomerFindNSortModel customerFindNSort);
        Task UpdateCustomer(CustomerUpdateModel customerUpdateModel);
        Task DeleteCustomer(CustomerDeleteModel customerDeleteModel);
    }
}
