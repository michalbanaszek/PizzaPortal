using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.ViewModels.Category;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPortal.WEB.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryMenu(ICategoryService categoryService, IMapper mapper)
        {
            this._categoryService = categoryService;
            this._mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            return View(this._mapper.Map<List<CategoryItemViewModel>>(this._categoryService.GetAllAsync().Result.OrderBy(x => x.Name)));
        }
    }
}
