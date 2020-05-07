using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Category;
using PizzaPortal.Model.ViewModels.Error;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, IMapper mapper, ILogger<CategoryController> logger)
        {
            this._categoryService = categoryService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new CategoryIndexViewModel()
            {
                Items = this._mapper.Map<List<CategoryItemViewModel>>(await this._categoryService.GetAllAsync())
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = this._mapper.Map<Category>(viewModel);

                var result = await this._categoryService.CreateAsync(category);

                if (result)
                {
                    return RedirectToAction(nameof(Details), new { id = category.Id });
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var category = await this._categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            return View(this._mapper.Map<CategoryEditViewModel>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CategoryEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = await this._categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    return View(viewModel);
                }

                var result = await this._categoryService.UpdateAsync(this._mapper.Map<Category>(viewModel));

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var category = await this._categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            return View(this._mapper.Map<CategoryDetailsViewModel>(category));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await this._categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return View("NotFound", NotFoundId(id));
            }

            return View(this._mapper.Map<CategoryDeleteViewModel>(category));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var category = await this._categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    return View("NotFound", NotFoundId(id));
                }

                var result = await this._categoryService.DeleteAsync(id);

                if (!result)
                {
                    this._logger.LogError($"Cannot delete this pizza, id: {id}");

                    ViewBag.ErrorMessage = $"Cannot delete this pizza, id: {id}";

                    return View("Error");
                }

                return RedirectToAction(nameof(Index));
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