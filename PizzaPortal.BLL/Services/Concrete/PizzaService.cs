using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Concrete
{
    public class PizzaService : Service<Pizza>, IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository) : base(pizzaRepository)
        {
            this._pizzaRepository = pizzaRepository;
        }

        public IEnumerable<Pizza> PreferredPizzas => this._pizzaRepository.PreferredPizzas;

        public async Task<IEnumerable<Pizza>> GetAllByCategoryAsync(string category)
        {
            return await this._pizzaRepository.GetAllByCategoryAsync(category);
        }
        public async Task<IEnumerable<Pizza>> GetAllIncludedAsync()
        {
            return await this._pizzaRepository.GetAllIncludedAsync();
        }
    }
}
