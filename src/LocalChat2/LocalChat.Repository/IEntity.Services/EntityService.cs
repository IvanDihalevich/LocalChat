using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.IEntity.Services
{
    public class EntityService<TEntity, TKey> : IEntityService<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        private List<TEntity> _entities;

        public EntityService()
        {
            _entities = new List<TEntity>();
        }

        public void AddEntity(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
            var existingEntity = _entities.FirstOrDefault(e => e.Id.Equals(entity.Id));
            if (existingEntity != null)
            {
                _entities.Remove(existingEntity);
                _entities.Add(entity);
            }
        }

        public void DeleteEntity(TKey entityId)
        {
            var entityToRemove = _entities.FirstOrDefault(e => e.Id.Equals(entityId));
            if (entityToRemove != null)
            {
                _entities.Remove(entityToRemove);
            }
        }

        public TEntity GetEntityById(TKey entityId)
        {
            return _entities.FirstOrDefault(e => e.Id.Equals(entityId));
        }

        public List<TEntity> GetAllEntities()
        {
            return _entities.ToList();
        }
    }
}
