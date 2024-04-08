using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.IEntity.Services
{
    public class EntityService<T, TKey> : IEntityService<T, TKey>
        where T : IEntity<TKey>
    {
        private List<T> _entities;

        public EntityService()
        {
            _entities = new List<T>();
        }

        public void AddEntity(T entity)
        {
            _entities.Add(entity);
        }

        public void UpdateEntity(T entity)
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

        public T GetEntityById(TKey entityId)
        {
            return _entities.FirstOrDefault(e => e.Id.Equals(entityId));
        }

        public List<T> GetAllEntities()
        {
            return _entities.ToList();
        }
    }
}
