using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaPortal.Model.ViewModels.Administration;
using PizzaPortal.Model.ViewModels.Administration.Role;
using PizzaPortal.Model.ViewModels.Administration.User;
using PizzaPortal.Model.ViewModels.Error;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AdministrationController> _logger;

        public AdministrationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, ILogger<AdministrationController> logger)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]     
        public IActionResult ListRoles()
        {
            RolesViewModel viewModel = new RolesViewModel()
            {
                Items = this._mapper.Map<List<RoleItemViewModel>>(this._roleManager.Roles)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View(new RoleCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await this._roleManager.CreateAsync(this._mapper.Map<IdentityRole>(viewModel));

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

                        return View(viewModel);
                    }
                }

                return RedirectToAction(nameof(ListRoles));
            }

            return View(viewModel);
        }

        [HttpGet]     
        public async Task<IActionResult> EditRole(string roleId)
        {
            var role = await this._roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return View("NotFound", NotFoundId(roleId));
            }

            var viewModel = this._mapper.Map<RoleEditViewModel>(role);

            foreach (var user in this._userManager.Users)
            {
                if (await this._userManager.IsInRoleAsync(user, role.Name))
                {
                    viewModel.Users.Add(user.UserName);
                }
            }

            return View(viewModel);
        }

        [HttpPost]   
        public async Task<IActionResult> EditRole(RoleEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await this._roleManager.FindByIdAsync(viewModel.Id);

                if (role == null)
                {
                    return View("NotFound", NotFoundId(viewModel.Id));
                }

                role.Name = viewModel.Name;

                var result = await this._roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

                        return View(viewModel);
                    }
                }

                return RedirectToAction(nameof(ListRoles));
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;

            var role = await this._roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return View("NotFound", NotFoundId(roleId));
            }

            var viewModel = new UsersRoleViewModel();

            foreach (var user in this._userManager.Users)
            {
                var userRoleViewModel = new UserRoleItemViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await this._userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                viewModel.Items.Add(userRoleViewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(string roleId, UsersRoleViewModel viewModel)
        {
            var role = await this._roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return View("NotFound", NotFoundId(roleId));
            }

            for (int i = 0; i < viewModel.Items.Count; i++)
            {
                var user = await this._userManager.FindByIdAsync(viewModel.Items[i].UserId);

                IdentityResult result = null;

                if (viewModel.Items[i].IsSelected && !(await this._userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await this._userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!viewModel.Items[i].IsSelected && await this._userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await this._userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (viewModel.Items.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { roleId = roleId });
                }
            }

            return RedirectToAction("EditRole", new { roleId = roleId });
        }

        [HttpPost]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await this._roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return View("NotFound", NotFoundId(roleId));
            }

            try
            {
                var result = await this._roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel()
                    {
                        ErrorTitle = $"Delete Role, {role.Id}",
                        ErrorMessage = $"Cannot delete this role, id: {role.Id}"
                    };

                    this._logger.LogError(errorViewModel.ErrorMessage);

                    return View("Error", errorViewModel);
                }

                return RedirectToAction(nameof(ListRoles));
            }
            catch (DbUpdateException ex)
            {
                this._logger.LogError(ex.Message);

                ErrorViewModel errorViewModel = new ErrorViewModel()
                {
                    ErrorTitle = $"{role.Name} role is in use",
                    ErrorMessage = $"{role.Name} role cannot be deleted as there are users in this role. " +
                    $"If you want to delete this role, please remove the users from the role and then try to delete"
                };

                return View("Error", errorViewModel);
            }
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            UsersViewModel viewModel = new UsersViewModel()
            {
                Items = this._mapper.Map<List<UserItemViewModel>>(this._userManager.Users)
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);

            if (user == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            var userRoles = await this._userManager.GetRolesAsync(user);
            var userClaims = await this._userManager.GetClaimsAsync(user);

            var viewModel = this._mapper.Map<UserEditViewModel>(user);
            viewModel.Roles = userRoles;
            viewModel.Claims = userClaims.Select(x => x.Type + " : " + x.Value).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserEditViewModel viewModel)
        {
            var user = await this._userManager.FindByIdAsync(viewModel.Id);

            if (user == null)
            {
                return View("NotFound", NotFoundId(viewModel.Id));
            }
            else
            {
                user.UserName = viewModel.UserName;

                var result = await this._userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);

            if (user == null)
            {
                return View("NotFound", NotFoundId(id));
            }
            else
            {
                var result = await this._userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListUsers));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(nameof(ListUsers));
            }
        }

        [HttpGet]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.UserId = userId;

            var user = await this._userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("NotFound", NotFoundId(userId));
            }

            var viewModel = new List<UserRolesViewModel>();

            foreach (var role in this._roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await this._userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                viewModel.Add(userRolesViewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> viewModel, string userId)
        {
            var user = await this._userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("NotFound", NotFoundId(userId));
            }

            var roles = await this._userManager.GetRolesAsync(user);
            var result = await this._userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(viewModel);
            }

            result = await this._userManager.AddToRolesAsync(user, viewModel.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(viewModel);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }

        [HttpGet]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await this._userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("NotFound", NotFoundId(userId));
            }

            var existingUserClaims = await this._userManager.GetClaimsAsync(user);

            var viewModel = new UserClaimsViewModel
            {
                UserId = userId
            };

            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };

                if (existingUserClaims.Any(c => c.Type == claim.Type && c.Value == "true"))
                {
                    userClaim.IsSelected = true;
                }

                viewModel.Claims.Add(userClaim);
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel viewModel)
        {
            var user = await this._userManager.FindByIdAsync(viewModel.UserId);

            if (user == null)
            {
                return View("NotFound", NotFoundId(viewModel.UserId));
            }

            var claims = await this._userManager.GetClaimsAsync(user);
            var result = await this._userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot remove user existing claims");
                return View(viewModel);
            }

            result = await this._userManager.AddClaimsAsync(user, viewModel.Claims.Select(c => new Claim(c.ClaimType, c.IsSelected ? "true" : "false")));

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Cannot add selected claims to user");
                return View(viewModel);
            }

            return RedirectToAction("EditUser", new { Id = viewModel.UserId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private static NotFoundViewModel NotFoundId(string id)
        {
            return new NotFoundViewModel()
            {
                StatusCode = 404,
                Message = $"Not found this id: {id}"
            };
        }
    }
}