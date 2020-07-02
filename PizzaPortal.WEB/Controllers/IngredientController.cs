using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Error;
using PizzaPortal.Model.ViewModels.Ingredient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    public class IngredientController : Controller
    {
        private readonly ILogger<IngredientController> _logger;
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;

        public IngredientController(ILogger<IngredientController> logger, IIngredientService ingredientService, IMapper mapper)
        {
            this._logger = logger;
            this._ingredientService = ingredientService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(new IngredientIndexViewModel()
            {
                Items = this._mapper.Map<List<IngredientItemViewModel>>(await this._ingredientService.GetAllAsync())
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var ingredient = await this._ingredientService.GetByIdAsync(id);

            if (ingredient == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            return View(this._mapper.Map<IngredientDetailViewModel>(await this._ingredientService.GetByIdAsync(id)));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IngredientCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await this._ingredientService.CheckByNameAsync(viewModel.Name);

                if (result)
                {
                    ModelState.AddModelError(string.Empty, "Ingredient is exist.");

                    return View(viewModel);
                }

                try
                {
                    var ingredient = this._mapper.Map<Ingredient>(viewModel);

                    var created = await this._ingredientService.CreateAsync(ingredient);

                    if (!created)
                    {
                        this._logger.LogError($"Cannot create ingredient");

                        return View("Error", new ErrorViewModel() { ErrorTitle = "Create Ingredient", ErrorMessage = "Cannot create ingredient" });
                    }

                    return RedirectToAction(nameof(Details), new { id = ingredient.Id });
                }
                catch (DbUpdateException ex)
                {
                    this._logger.LogError(ex.Message);

                    return View("Error", new ErrorViewModel() { ErrorTitle = "Create Ingredient", ErrorMessage = ex.Message });
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var ingredient = await this._ingredientService.GetByIdAsync(id);

            if (ingredient == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            return View(this._mapper.Map<IngredientEditViewModel>(ingredient));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, IngredientEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var ingredient = await this._ingredientService.GetByIdAsync(viewModel.Id);

                if (ingredient == null)
                {
                    return View("NotFound", NotFoundId(id));
                }

                try
                {
                    var updated = await this._ingredientService.UpdateAsync(this._mapper.Map<Ingredient>(viewModel));

                    if (!updated)
                    {
                        this._logger.LogError($"Cannot update ingredient");

                        return View("Error", new ErrorViewModel() { ErrorTitle = "Edit Ingredient", ErrorMessage = "Cannot update ingredient" });
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    this._logger.LogError(ex.Message);

                    return View("Error", new ErrorViewModel() { ErrorTitle = "Edit Ingredient", ErrorMessage = ex.Message });
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var ingredient = await this._ingredientService.GetByIdAsync(id);

            if (ingredient == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            return View(this._mapper.Map<IngredientDeleteViewModel>(ingredient));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var ingredient = await this._ingredientService.GetByIdAsync(id);

            if (ingredient == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            try
            {
                var deleted = await this._ingredientService.DeleteAsync(id);

                if (!deleted)
                {
                    this._logger.LogError($"Cannot delete ingredient");

                    return View("Error", new ErrorViewModel() { ErrorTitle = "Delete Ingredient", ErrorMessage = "Cannot delete ingredient" });
                }

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                this._logger.LogError(ex.Message);

                return View("Error", new ErrorViewModel() { ErrorTitle = "Delete Ingredient", ErrorMessage = ex.Message });
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
