﻿using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IPizzaIngredientService : IService<PizzaIngredient>
    {
        Task<List<PizzaIngredient>> GetByAllWithIncludeAsync();
        Task<PizzaIngredient> GetByIdWithIncludeAsync(string id);
        Task<bool> CheckIngredientIsExistInPizzaAsync(string pizzaId, string ingredientId);
        Task<List<string>> GetAllIngredientInPizzaAsync(string pizzaId);
        Task<bool> RemoveAllIngredientInPizzaAsync(string pizzaId);
    }
}
