using Microsoft.EntityFrameworkCore;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Migrations;
using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            this._context = context;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            this._context.Set<TEntity>().Add(entity);
            var added = await this._context.SaveChangesAsync();
            return added > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await this._context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            this._context.Entry(entity).State = EntityState.Detached;
            this._context.Entry(entity).State = EntityState.Deleted;           

            var deleted = await this._context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this._context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await this._context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var entityExist = await this._context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == entity.Id);

            if (entityExist == null)
            {
                return false;
            }

            this._context.Entry(entityExist).State = EntityState.Detached;
            this._context.Entry(entity).State = EntityState.Modified;

            var updated = await this._context.SaveChangesAsync();
            return updated > 0;
        }
    }
}
