using DevPace.Domain.Entities;
using DevPace.Domain.Models.CustomerModels;

namespace DevPace.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer?> GetCustomer(Guid id);
        Task<IEnumerable<Customer>> GetCustomers(CustomerFindNSortModel customerFindNSortModel);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(Customer customer);
        bool UniqueName(string name);
        bool UniqueName(string name, Guid id);
        bool UniqueEmail(string email);
        bool UniqueEmail(string email, Guid id);
        bool UniquePhoneNumber(string phoneNumber);
        bool UniquePhoneNumber(string phoneNumber, Guid id);
        bool ExistsId(Guid id);
    }
}
