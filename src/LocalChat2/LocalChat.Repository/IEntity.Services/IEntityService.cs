using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.IEntity.Services
{
    public interface IEntityService<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        void AddEntity(TEntity entity);
        void UpdateEntity(TEntity entity);
        void DeleteEntity(TKey entityId);
        TEntity GetEntityById(TKey entityId);
        List<TEntity> GetAllEntities();
    }
}
