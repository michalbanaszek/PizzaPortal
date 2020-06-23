using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Category;
using PizzaPortal.Model.ViewModels.Error;
using PizzaPortal.Model.ViewModels.Pizza;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    public class PizzaController : Controller
    {
        private readonly IPizzaService _pizzaService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(IPizzaService pizzaService, ICategoryService categoryService, IMapper mapper, IHostingEnvironment hostingEnvironment, ILogger<PizzaController> logger)
        {
            this._pizzaService = pizzaService;
            this._categoryService = categoryService;
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
                var errorViewModel = new NotFoundViewModel()
                {
                    StatusCode = 404,
                    Message = $"Not found this id: {id}"
                };

                return View("NotFound", errorViewModel);
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

                        ViewBag.ErrorMessage = $"Cannot create pizza";

                        return View("Error");
                    }

                    return RedirectToAction(nameof(Details), new { id = pizza.Id });

                }
                catch (DbUpdateException ex)
                {
                    this._logger.LogError(ex.Message);

                    return View("Error");
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
                var errorViewModel = new NotFoundViewModel()
                {
                    StatusCode = 404,
                    Message = $"Not found this id: {id}"
                };

                return View("NotFound", errorViewModel);
            }

            var viewModel = this._mapper.Map<PizzaEditViewModel>(pizza);
            viewModel.Categories = this._mapper.Map<List<CategoryItemViewModel>>(await this._categoryService.GetAllAsync());

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
                        var errorViewModel = new NotFoundViewModel()
                        {
                            StatusCode = 404,
                            Message = $"Not found this id: {id}"
                        };

                        return View("NotFound", errorViewModel);
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

                        ViewBag.ErrorMessage = $"Cannot update this pizza, id: {id}";

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

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var pizza = await this._pizzaService.GetByIdAsync(id);

            if (pizza == null)
            {
                var errorViewModel = new NotFoundViewModel()
                {
                    StatusCode = 404,
                    Message = $"Not found this id: {id}"
                };

                return View("NotFound", errorViewModel);
            }

            return View(this._mapper.Map<PizzaDeleteViewModel>(pizza));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(string id)
        {
                var pizza = await this._pizzaService.GetByIdAsync(id);

                if (pizza == null)
                {
                    var errorViewModel = new NotFoundViewModel()
                    {
                        StatusCode = 404,
                        Message = $"Not found this id: {id}"
                    };

                    return View("NotFound", errorViewModel);
                }

            try
            {
                var result = await this._pizzaService.DeleteAsync(id);

                if (!result)
                {
                    this._logger.LogError($"Cannot delete this pizza, id: {id}");

                    ViewBag.ErrorMessage = $"Cannot delete this pizza, id: {id}";

                    return View("Error");
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

                return View("Error");
            }
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
    }
}