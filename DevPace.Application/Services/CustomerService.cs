using AutoMapper;
using DevPace.Application.Helpers;
using DevPace.Application.Services.Interfaces;
using DevPace.Domain.Entities;
using DevPace.Domain.Exceptions.CustomerExceptions;
using DevPace.Domain.Interfaces;
using DevPace.Domain.Models.CustomerModels;

namespace DevPace.Application.Services
{
    public class CustomerService : ICustomerService
    {
        #region Dependencies

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        #endregion

        #region .ctor

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementation

        public async Task<CustomerModel> CreateCustomer(CustomerCreateModel customerCreateModel)
        {

            if (!_customerRepository.UniqueName(customerCreateModel.Name))
            {
                throw new FieldNotUniqueException(nameof(customerCreateModel.Name), customerCreateModel.Name);
            }

            if (!_customerRepository.UniqueEmail(customerCreateModel.Email))
            {
                throw new FieldNotUniqueException(nameof(customerCreateModel.Email), customerCreateModel.Email);
            }

            if (!_customerRepository.UniquePhoneNumber(customerCreateModel.PhoneNumber))
            {
                throw new FieldNotUniqueException(nameof(customerCreateModel.PhoneNumber), customerCreateModel.PhoneNumber);
            }

            var customer = await _customerRepository.CreateCustomer(_mapper.Map<Customer>(customerCreateModel));

            return _mapper.Map<CustomerModel>(customer);
        }

        public async Task<CustomerModel?> GetCustomer(Guid id)
        {
            var customer = await _customerRepository.GetCustomer(id);

            return _mapper.Map<CustomerModel?>(customer);
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomers(CustomerFindNSortModel customerFindNSortModel)
        {
            ValidationHelper.ValidateCustomersQueryParams(customerFindNSortModel);

            var customers = await _customerRepository.GetCustomers(customerFindNSortModel);

            return _mapper.Map<IEnumerable<CustomerModel>>(customers);
        }

        public async Task UpdateCustomer(CustomerUpdateModel customerUpdateModel)
        {
            if (!_customerRepository.ExistsId(customerUpdateModel.Id))
            {
                throw new CustomerIdNotFoundException(customerUpdateModel.Id);
            }

            if (!_customerRepository.UniqueName(customerUpdateModel.Name, customerUpdateModel.Id))
            {
                throw new FieldNotUniqueException(nameof(customerUpdateModel.Name), customerUpdateModel.Name);
            }

            if (!_customerRepository.UniqueEmail(customerUpdateModel.Email, customerUpdateModel.Id))
            {
                throw new FieldNotUniqueException(nameof(customerUpdateModel.Email), customerUpdateModel.Email);
            }

            if (!_customerRepository.UniquePhoneNumber(customerUpdateModel.PhoneNumber, customerUpdateModel.Id))
            {
                throw new FieldNotUniqueException(nameof(customerUpdateModel.PhoneNumber), customerUpdateModel.PhoneNumber);
            }

            await _customerRepository.UpdateCustomer(_mapper.Map<Customer>(customerUpdateModel));
        }

        public async Task DeleteCustomer(CustomerDeleteModel customerDeleteModel)
        {
            if (!_customerRepository.ExistsId(customerDeleteModel.Id))
            {
                throw new CustomerIdNotFoundException(customerDeleteModel.Id);
            }

            await _customerRepository.DeleteCustomer(_mapper.Map<Customer>(customerDeleteModel));
        }

        #endregion
    }
}
