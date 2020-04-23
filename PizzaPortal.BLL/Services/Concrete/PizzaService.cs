using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Model.Models;
using System.Collections.Generic;

namespace PizzaPortal.BLL.Services.Concrete
{
    public class PizzaService : Service<PizzaDTO>, IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository) : base(pizzaRepository)
        {
            this._pizzaRepository = pizzaRepository;
        }

        public IEnumerable<PizzaDTO> PreferredPizzas => this._pizzaRepository.PreferredPizzas;

        public IEnumerable<PizzaDTO> GetAllByCategory(string category)
        {
            return this._pizzaRepository.GetAllByCategory(category);
        }
    }
}
