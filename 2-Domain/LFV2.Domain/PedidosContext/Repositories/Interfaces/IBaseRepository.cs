using LFV2.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LFV2.Domain.PedidosContext.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : IEntity
    {
        void Create(TEntity entity);
        bool Delete(TEntity entity);
        void Delete(int id);
        void Edit(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> Filter();
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);
        void SaveChanges();

        Task CreateAsync(TEntity entity);

        Task SaveChangesAsync();
    }
}
