using AutoMapper;
using FunBooksAndVideos.Application.Features.Customer.Commands.CreateCustomerAccount;
using FunBooksAndVideos.Application.Features.Customer.Queries.Dtos;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.MappingProfiles;

public class CustomerAccountProfile: Profile
{
    public CustomerAccountProfile()
    {
        CreateMap<CustomerAccount, CustomerAccountDto>()
            .ForMember(c => c.MembershipTypeString,
                opt => opt.MapFrom(src => src.MembershipType.ToString()));
            
        CreateMap<CreateCustomerAccountCommand, CustomerAccount>();
    }
}
