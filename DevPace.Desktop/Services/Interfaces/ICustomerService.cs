using DevPace.Desktop.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPace.Desktop.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerModel> GetCustomerByIdAsync(Guid id);
        Task<IEnumerable<CustomerModel>> GetCustomersAsync(int pageNumber, int rowCount);
        Task<IEnumerable<CustomerModel>> GetCustomersAsync(int pageNumber, int rowCount, string searchItem, string orderBy, bool orderByDescending);
        Task<CustomerModel> CreateCustomerAsync(CustomerCreateModel customerCreate);
        Task UpdateCustomerAsync(CustomerUpdateModel customerUpdate);
        Task DeleteCustomerAsync(CustomerDeleteModel customerDelete);
    }
}
