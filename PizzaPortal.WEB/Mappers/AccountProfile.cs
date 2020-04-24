using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PizzaPortal.Model.ViewModels.Account;

namespace PizzaPortal.WEB.Mappers
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterViewModel, IdentityUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
