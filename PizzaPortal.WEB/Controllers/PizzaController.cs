using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Error;
using PizzaPortal.Model.ViewModels.Pizza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    public class PizzaController : Controller
    {
        private readonly IPizzaService _pizzaService;
        private readonly IMapper _mapper;
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(IPizzaService pizzaService, IMapper mapper, ILogger<PizzaController> logger)
        {
            this._pizzaService = pizzaService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string category)
        {
            IEnumerable<PizzaDTO> pizzas;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                pizzas = await this._pizzaService.GetAllAsync();
                currentCategory = "All pizzas";
            }
            else
            {
                pizzas = this._pizzaService.GetAllByCategory(category);
                currentCategory = category;
            }

            var viewModel = new IndexPizzaViewModel()
            {
                Items = this._mapper.Map<List<ItemPizzaViewModel>>(pizzas),
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

            return View(this._mapper.Map<DetailsPizzaViewModel>(pizza));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePizzaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var pizza = this._mapper.Map<PizzaDTO>(viewModel);

                    var result = await this._pizzaService.CreateAsync(pizza);

                    if (result)
                    {
                        return RedirectToAction(nameof(Details), new { id = pizza.Id });
                    }

                    return View(viewModel);
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

            return View(this._mapper.Map<EditPizzaViewModel>(pizza));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, EditPizzaViewModel viewModel)
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

                    var pizzatoUpdate = this._mapper.Map<PizzaDTO>(viewModel);

                    var result = await this._pizzaService.UpdateAsync(pizzatoUpdate);

                    if (result)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    return View(viewModel);
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

            return View(this._mapper.Map<DeletePizzaViewModel>(pizza));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id, DeletePizzaViewModel viewModel)
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

                var result = await this._pizzaService.DeleteAsync(id);

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(viewModel);
            }
            catch (DbUpdateException ex)
            {
                this._logger.LogError(ex.Message);

                return View("Error");
            }
        }
    }
}