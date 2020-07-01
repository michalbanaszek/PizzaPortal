using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Error;
using PizzaPortal.Model.ViewModels.PizzaIngredient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    public class PizzaIngredientController : Controller
    {
        private readonly ILogger<PizzaIngredientController> _logger;
        private readonly IMapper _mapper;
        private readonly IPizzaIngredientService _pizzaIngredientService;
        private readonly IPizzaService _pizzaService;
        private readonly IIngredientService _ingredientService;

        public PizzaIngredientController(ILogger<PizzaIngredientController> logger, IMapper mapper, IPizzaIngredientService pizzaIngredientService, IPizzaService pizzaService, IIngredientService ingredientService)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._pizzaIngredientService = pizzaIngredientService;
            this._pizzaService = pizzaService;
            this._ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(new PizzaIngredientIndexViewModel() { Items = this._mapper.Map<List<PizzaIngredientItemViewModel>>(await this._pizzaIngredientService.GetByAllWithIncludeAsync()) });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var item = await this._pizzaIngredientService.GetByIdAsync(id);

            if (item == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            return View(this._mapper.Map<PizzaIngredientDetailViewModel>(await this._pizzaIngredientService.GetByIdWithIncludeAsync(id)));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var pizzaIngredient = await this._pizzaIngredientService.GetByIdAsync(id);

            if (pizzaIngredient == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            ViewBag.PizzaId = new SelectList(await this._pizzaService.GetAllAsync(), "Id", "Name", pizzaIngredient.PizzaId);
            ViewBag.IngredientId = new SelectList(await this._ingredientService.GetAllAsync(), "Id", "Name", pizzaIngredient.IngredientId);

            return View(this._mapper.Map<PizzaIngredientEditViewModel>(pizzaIngredient));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PizzaIngredientEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var pizzaIngredient = await this._pizzaIngredientService.GetByIdAsync(id);

                if (pizzaIngredient == null)
                {
                    return View("NotFound", NotFoundId(id));
                }

                var isExist = await this._pizzaIngredientService.CheckIngredientIsExistInPizzaAsync(viewModel.PizzaId, viewModel.IngredientId);

                if (isExist)
                {
                    ModelState.AddModelError(string.Empty, "Ingredient in this pizza is exist.");

                    ViewBag.PizzaId = new SelectList(await this._pizzaService.GetAllAsync(), "Id", "Name", pizzaIngredient.PizzaId);
                    ViewBag.IngredientId = new SelectList(await this._ingredientService.GetAllAsync(), "Id", "Name", pizzaIngredient.IngredientId);

                    return View(viewModel);
                }

                try
                {
                    var updated = await this._pizzaIngredientService.UpdateAsync(this._mapper.Map<PizzaIngredient>(viewModel));

                    if (!updated)
                    {
                        this._logger.LogError("Cannot Edit PizzaIngredient");

                        return View("Error", new ErrorViewModel() { ErrorTitle = "Edit PizzaIngredient", ErrorMessage = "Cannot Edit PizzaIngredient" });
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    this._logger.LogError(ex.Message);

                    return View("Error", new ErrorViewModel() { ErrorTitle = "Edit PizzaIngredient", ErrorMessage = ex.Message });
                }
            }

            ViewBag.PizzaId = new SelectList(await this._pizzaService.GetAllAsync(), "Id", "Name", viewModel.PizzaId);
            ViewBag.IngredientId = new SelectList(await this._ingredientService.GetAllAsync(), "Id", "Name", viewModel.IngredientId);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.PizzaId = new SelectList(await this._pizzaService.GetAllAsync(), "Id", "Name");
            ViewBag.IngredientId = new SelectList(await this._ingredientService.GetAllAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PizzaIngredientCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var isExist = await this._pizzaIngredientService.CheckIngredientIsExistInPizzaAsync(viewModel.PizzaId, viewModel.IngredientId);

                if (isExist)
                {
                    ModelState.AddModelError(string.Empty, "Ingredient in this pizza is exist.");

                    ViewBag.PizzaId = new SelectList(await this._pizzaService.GetAllAsync(), "Id", "Name", viewModel.PizzaId);
                    ViewBag.IngredientId = new SelectList(await this._ingredientService.GetAllAsync(), "Id", "Name", viewModel.IngredientId);

                    return View(viewModel);
                }

                try
                {
                    var created = await this._pizzaIngredientService.CreateAsync(this._mapper.Map<PizzaIngredient>(viewModel));

                    if (!created)
                    {
                        this._logger.LogError("Cannot Create PizzaIngredient");

                        return View("Error", new ErrorViewModel() { ErrorTitle = "Create PizzaIngredient", ErrorMessage = "Cannot Create PizzaIngredient" });
                    }
                }
                catch (DbUpdateException ex)
                {
                    this._logger.LogError(ex.Message);

                    return View("Error", new ErrorViewModel() { ErrorTitle = "Create PizzaIngredient", ErrorMessage = ex.Message });
                }

                return RedirectToAction(nameof(Index));
            }


            ViewBag.PizzaId = new SelectList(await this._pizzaService.GetAllAsync(), "Id", "Name", viewModel.PizzaId);
            ViewBag.IngredientId = new SelectList(await this._ingredientService.GetAllAsync(), "Id", "Name", viewModel.IngredientId);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var pizzaIngredient = await this._pizzaIngredientService.GetByIdWithIncludeAsync(id);

            if (pizzaIngredient == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            return View(this._mapper.Map<PizzaIngredientDeleteViewModel>(pizzaIngredient));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var pizzaIngredient = await this._pizzaIngredientService.GetByIdWithIncludeAsync(id);

            if (pizzaIngredient == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            try
            {
                var deleted = await this._pizzaIngredientService.DeleteAsync(id);

                if (!deleted)
                {
                    this._logger.LogError("Cannot Delete PizzaIngredient");

                    return View("Error", new ErrorViewModel() { ErrorTitle = "Delete exception PizzaIngredient", ErrorMessage = "Cannot Delete PizzaIngredient" });
                }
            }
            catch (DbUpdateException ex)
            {
                this._logger.LogError(ex.Message);

                return View("Error", new ErrorViewModel() { ErrorTitle = "Delete exception PizzaIngredient", ErrorMessage = ex.Message });
            }

            return RedirectToAction(nameof(Index));
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
