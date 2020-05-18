using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PizzaPortal.Model.ViewModels.Administration.Role;

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
        }
    }
}
