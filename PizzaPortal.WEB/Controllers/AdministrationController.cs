using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaPortal.Model.ViewModels.Administration.Role;
using PizzaPortal.Model.ViewModels.Error;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
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
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            try
            {
                var role = await this._roleManager.FindByIdAsync(roleId);

                if (role == null)
                {
                    return View("NotFound", NotFoundId(roleId));
                }

                var result = await this._roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    this._logger.LogError($"Cannot delete this role, id: {role.Id}");

                    ViewBag.ErrorMessage = $"Cannot delete this role, id: {role.Id}";

                    return View("Error");
                }

                return RedirectToAction(nameof(ListRoles));
            }
            catch (DbUpdateException ex)
            {
                this._logger.LogError(ex.Message);

                return View("Error");
            }
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