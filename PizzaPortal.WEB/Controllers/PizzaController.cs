using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Category;
using PizzaPortal.Model.ViewModels.Error;
using PizzaPortal.Model.ViewModels.Ingredient;
using PizzaPortal.Model.ViewModels.Pizza;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    public class PizzaController : Controller
    {
        private readonly IPizzaService _pizzaService;
        private readonly ICategoryService _categoryService;
        private readonly IIngredientService _ingredientService;
        private readonly IPizzaIngredientService _pizzaIngredientService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(IPizzaService pizzaService, ICategoryService categoryService, IIngredientService ingredientService, IPizzaIngredientService pizzaIngredientService, IMapper mapper, IHostingEnvironment hostingEnvironment, ILogger<PizzaController> logger)
        {
            this._pizzaService = pizzaService;
            this._categoryService = categoryService;
            this._ingredientService = ingredientService;
            this._pizzaIngredientService = pizzaIngredientService;
            this._mapper = mapper;
            this._hostingEnvironment = hostingEnvironment;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string category)
        {
            IEnumerable<Pizza> pizzas;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                pizzas = await this._pizzaService.GetAllIncludedAsync();
                currentCategory = "All pizzas";
            }
            else
            {
                pizzas = await this._pizzaService.GetAllByCategoryAsync(category);
                currentCategory = category;
            }

            var viewModel = new PizzaIndexViewModel()
            {
                Items = this._mapper.Map<List<PizzaItemViewModel>>(pizzas),
                CurrentCategory = currentCategory
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            var pizza = await this._pizzaService.GetByIdAsync(id);

            if (pizza == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            return View(this._mapper.Map<PizzaDetailsViewModel>(pizza));
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var viewModel = new PizzaCreateViewModel();
            viewModel.Categories = this._mapper.Map<List<CategoryItemViewModel>>(await this._categoryService.GetAllAsync());

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PizzaCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = ProccessUploadedFile(viewModel);

                    var pizza = this._mapper.Map<Pizza>(viewModel);
                    pizza.PhotoPath = uniqueFileName;

                    var result = await this._pizzaService.CreateAsync(pizza);

                    if (!result)
                    {
                        this._logger.LogError($"Cannot create pizza");

                        return View("Error", new ErrorViewModel() { ErrorTitle = "Create Pizza", ErrorMessage = "Cannot create pizza" });
                    }

                    return RedirectToAction(nameof(Details), new { id = pizza.Id });

                }
                catch (DbUpdateException ex)
                {
                    this._logger.LogError(ex.Message);

                    return View("Error", new ErrorViewModel() { ErrorTitle = "Create Pizza", ErrorMessage = ex.Message });
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var pizza = await this._pizzaService.GetByIdAsync(id);

            if (pizza == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            var viewModel = this._mapper.Map<PizzaEditViewModel>(pizza);
            viewModel.Categories = this._mapper.Map<List<CategoryItemViewModel>>(await this._categoryService.GetAllAsync());
            viewModel.Ingredients = await this._pizzaIngredientService.GetAllIngredientInPizzaAsync(id);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, PizzaEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pizza = await this._pizzaService.GetByIdAsync(id);

                    if (pizza == null)
                    {
                        return View("NotFound", NotFoundId(id));
                    }

                    var pizzatoUpdate = this._mapper.Map<Pizza>(viewModel);

                    if (viewModel.Photo != null)
                    {
                        if (!string.IsNullOrEmpty(viewModel.ExistingPhotoPath))
                        {
                            ProccessDeletedFile(viewModel.ExistingPhotoPath);
                        }

                        pizzatoUpdate.PhotoPath = ProccessUploadedFile(viewModel);
                    }

                    var result = await this._pizzaService.UpdateAsync(pizzatoUpdate);

                    if (!result)
                    {
                        this._logger.LogError($"Cannot update this pizza, id: {id}");

                        return View("Error", new ErrorViewModel() { ErrorTitle = "Update Pizza", ErrorMessage = $"Cannot update this pizza, id: {id}" });
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    this._logger.LogError(ex.Message);

                    return View("Error", new ErrorViewModel() { ErrorTitle = "Update Pizza", ErrorMessage = ex.Message });
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var pizza = await this._pizzaService.GetByIdAsync(id);

            if (pizza == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            return View(this._mapper.Map<PizzaDeleteViewModel>(pizza));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(string id)
        {
            var pizza = await this._pizzaService.GetByIdAsync(id);

            if (pizza == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            try
            {
                var result = await this._pizzaService.DeleteAsync(id);

                if (!result)
                {
                    this._logger.LogError($"Cannot delete this pizza, id: {id}");

                    return View("Error", new ErrorViewModel() { ErrorTitle = "Delete Pizza", ErrorMessage = $"Cannot delete this pizza, id: {id}" });
                }

                if (!string.IsNullOrEmpty(pizza.PhotoPath))
                {
                    ProccessDeletedFile(pizza.PhotoPath);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                this._logger.LogError(ex.Message);

                return View("Error", new ErrorViewModel() { ErrorTitle = "Delete Pizza", ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManagePizzaIngredients(string pizzaId)
        {
            ViewBag.PizzaId = pizzaId;

            var pizza = await this._pizzaService.GetByIdAsync(pizzaId);

            if (pizza == null)
            {
                return View("NotFound", NotFoundId(pizzaId));
            }

            var viewModel = new List<PizzaIngredientsViewModel>();

            foreach (var ingredientItem in await this._ingredientService.GetAllAsync())
            {
                var pizzaIngredientViewModel = new PizzaIngredientsViewModel()
                {
                    IngredientId = ingredientItem.Id,
                    Name = ingredientItem.Name                     
                };

                if (await this._pizzaIngredientService.CheckIngredientIsExistInPizzaAsync(pizzaId, ingredientItem.Id))
                {
                    pizzaIngredientViewModel.IsSelected = true;
                }
                else
                {
                    pizzaIngredientViewModel.IsSelected = false;
                }

                viewModel.Add(pizzaIngredientViewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ManagePizzaIngredients(List<PizzaIngredientsViewModel> viewModel, string pizzaId)
        {
            var pizza = await this._pizzaService.GetByIdAsync(pizzaId);

            if (pizza == null)
            {
                return View("NotFound", NotFoundId(pizzaId));
            }

            try
            {
                var result = await this._pizzaIngredientService.RemoveAllIngredientInPizzaAsync(pizzaId);

                if (!result)
                {
                    ModelState.AddModelError("", "Cannot remove ingredient");
                    return View(viewModel);
                }

                var selectedIngredients = viewModel.Where(x => x.IsSelected).Select(x => x.IngredientId);

                foreach (var ingredientItem in selectedIngredients)
                {
                    await this._pizzaIngredientService.CreateAsync(new PizzaIngredient() { IngredientId = ingredientItem, PizzaId = pizzaId });
                }
            }
            catch (DbUpdateException ex)
            {
                this._logger.LogError(ex.Message);

                return View("Error", new ErrorViewModel() { ErrorTitle = "Manage Pizza Ingredients", ErrorMessage = ex.Message });
            }

            return RedirectToAction(nameof(Edit), new { Id = pizzaId });
        }

        private string ProccessUploadedFile(PizzaCreateViewModel viewModel)
        {
            string uniqueFileName = null;

            if (viewModel.Photo != null)
            {
                var uploadsFolder = Path.Combine(this._hostingEnvironment.WebRootPath, "images", "pizzas");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.Photo.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        private void ProccessDeletedFile(string fileToDelete)
        {
            var filePath = Path.Combine(this._hostingEnvironment.WebRootPath, "images", "pizzas", fileToDelete);

            System.IO.File.Delete(filePath);
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