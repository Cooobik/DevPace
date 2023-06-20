using DevPace.Domain.Entities;
using DevPace.Domain.Exceptions.CustomerExceptions;
using DevPace.Domain.Interfaces;
using DevPace.Domain.Models.CustomerModels;
using DevPace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevPace.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        #region Dependencies

        private readonly DataContext _dataContext;

        #endregion

        #region .ctor

        public CustomerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region CRUD

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            await _dataContext.Customers.AddAsync(customer);

            await _dataContext.SaveChangesAsync();
            
            return customer;
        }

        public async Task<Customer?> GetCustomer(Guid id)
        {

            return await _dataContext.Customers.FindAsync(id) ?? throw new CustomerIdNotFoundException(id);
        }

        public async Task<IEnumerable<Customer>> GetCustomers(CustomerFindNSortModel customerFindNSortModel)
        {
            var searchItem = string.IsNullOrEmpty(customerFindNSortModel.SearchItem) ? "%" : $"%{customerFindNSortModel.SearchItem}%";
            var orderBy = customerFindNSortModel.OrderBy ?? string.Empty;
            var orderDescending = customerFindNSortModel.OrderDescending ? true : false;
            var pageSize = customerFindNSortModel.RowCount;
            var pageNumber = customerFindNSortModel.PageNumber;

            var query = _dataContext.Customers.FromSqlInterpolated(
                $@"EXECUTE [dbo].[CustomerFindAndSortProcedure]
                    @SearchItem = {searchItem},
                    @OrderBy = {orderBy},
                    @OrderDescending = {orderDescending},
                    @RowCount = {pageSize},
                    @PageNumber = {pageNumber}");

            return await query.ToListAsync();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            _dataContext.Update(customer);

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteCustomer(Customer customer)
        {
            _dataContext.Remove(customer);

            await _dataContext.SaveChangesAsync();
        }

        #endregion

        #region IsUnique/Exist

        public bool UniqueName(string name)
        {
            return !_dataContext.Customers.Any(n => n.Name.Equals(name));
        }

        public bool UniqueName(string name, Guid id)
        {
            return !_dataContext.Customers.Any(n => n.Name.Equals(name) && n.Id != id);
        }

        public bool UniqueEmail(string email)
        {
            return !_dataContext.Customers.Any(e => e.Email.Equals(email));
        }

        public bool UniqueEmail(string email, Guid id)
        {
            return !_dataContext.Customers.Any(e => e.Email.Equals(email) && e.Id != id);
        }

        public bool UniquePhoneNumber(string phoneNumber)
        {
            return !_dataContext.Customers.Any(pn => pn.PhoneNumber.Equals(phoneNumber));
        }

        public bool UniquePhoneNumber(string phoneNumber, Guid id)
        {
            return !_dataContext.Customers.Any(pn => pn.PhoneNumber.Equals(phoneNumber) && pn.Id != id);
        }

        public bool ExistsId(Guid id)
        {
            return _dataContext.Customers.Any(i => i.Id.Equals(id));
        }

        #endregion
    }
}
