
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Concrete
{
    public class Service<TEntity> : IService<TEntity> where TEntity : BaseModelDTO
    {
        private readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository)
        {
            this._repository = repository;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await this._repository.CreateAsync(entity);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await this._repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this._repository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await this._repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await this._repository.UpdateAsync(entity);
        }
    }
}
