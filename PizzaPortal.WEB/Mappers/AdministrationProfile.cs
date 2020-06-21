using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PizzaPortal.Model.ViewModels.Administration.Role;
using PizzaPortal.Model.ViewModels.Administration.User;

namespace PizzaPortal.WEB.Mappers
{
    public class AdministrationProfile : Profile
    {
        public AdministrationProfile()
        {
            CreateMap<RoleCreateViewModel, IdentityRole>();
            CreateMap<IdentityUser, UserRoleItemViewModel>();
            CreateMap<IdentityRole, RoleItemViewModel>();
            CreateMap<RoleEditViewModel, IdentityRole>().ReverseMap();

            CreateMap<IdentityUser, UserItemViewModel>();
            CreateMap<IdentityUser, UserEditViewModel>();
        }
    }
}
