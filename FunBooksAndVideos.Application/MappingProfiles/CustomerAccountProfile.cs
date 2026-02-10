using AutoMapper;
using FunBooksAndVideos.Application.Features.Customer.Commands.CreateCustomerAccount;
using FunBooksAndVideos.Application.Features.Customer.Queries.Dtos;
using FunBooksAndVideos.Domain;

namespace FunBooksAndVideos.Application.MappingProfiles;

internal class CustomerAccountProfile: Profile
{
    public CustomerAccountProfile()
    {
        CreateMap<CustomerAccount, CustomerAccountDto>()
            .ForMember(c => c.MembershipTypeString,
                opt => opt.MapFrom(src => src.MembershipTypes.ToString()));
            
        CreateMap<CreateCustomerAccountCommand, CustomerAccount>();
    }
}
