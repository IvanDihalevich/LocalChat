using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.IEntity.Services
{
    public interface IEntityService<T, TKey>
        where T : IEntity<TKey>
    {
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(TKey entityId);
        T GetEntityById(TKey entityId);
        List<T> GetAllEntities();
    }
}
