using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.IEntity.Services
{
    public class EntityService<TEntity, TKey> : IEntityService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        //private List<TEntity> _entities;
        protected ChatDbContext _ctx;

        public EntityService(ChatDbContext ctx)
        {
            _ctx = ctx;
            //_entities = new List<TEntity>();
        }

        public void AddEntity(TEntity entity)
        {
            _ctx.Add(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _ctx.Set<TEntity>().ToListAsync();

        public virtual async Task CreateAsync(TEntity entity)
        {
            await _ctx.Set<TEntity>().AddAsync(entity);
            await SaveAsync();
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            _ctx.Set<TEntity>().Update(entity);
            await SaveAsync();
        }
        public virtual async Task DeleteAsync(TKey id)
        {
            _ctx.Set<TEntity>().Remove(await _ctx.Set<TEntity>().FindAsync(id));
            await SaveAsync();

        }
        public virtual async Task<TEntity> GetAsync(TKey id)
        {
            return await _ctx.Set<TEntity>().FindAsync(id);
        }

        public async Task SaveAsync() => await _ctx.SaveChangesAsync();
    }
}
