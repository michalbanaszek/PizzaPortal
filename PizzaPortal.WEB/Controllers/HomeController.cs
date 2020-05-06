using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.ViewModels.Home;
using PizzaPortal.Model.ViewModels.Pizza;
using System.Collections.Generic;

namespace PizzaPortal.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaService _pizzaService;
        private readonly IMapper _mapper;

        public HomeController(IPizzaService pizzaService, IMapper mapper)
        {
            this._pizzaService = pizzaService;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel()
            {
                PrefferedPizza = this._mapper.Map<List<PizzaItemViewModel>>(this._pizzaService.PreferredPizzas)
            };

            return View(viewModel);
        }
    }
}
