using AutoMapper;
using DevPace.Domain.Entities;
using DevPace.Domain.Models.CustomerModels;

namespace DevPace.Application.Mappers
{
    public sealed class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<Customer, CustomerModel>();

            CreateMap<Customer, CustomerCreateModel>()
                .ReverseMap();

            CreateMap<CustomerUpdateModel, Customer>();

            CreateMap<CustomerDeleteModel, Customer>();
        }
    }
}
