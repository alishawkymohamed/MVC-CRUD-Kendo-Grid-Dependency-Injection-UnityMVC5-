using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IRepositories.IRepositories
{
    public interface _IGenericRepository<TDbEntity> where TDbEntity : _BaseEntity
    {
        IQueryable<TDbEntity> GetAll(bool WithTracking = true);
        TDbEntity GetById(object Id);
        IEnumerable<TDbEntity> Insert(IEnumerable<TDbEntity> Entities);
        IEnumerable<object> Delete(IEnumerable<object> Ids);
        void Update(TDbEntity Entity);
        object[] GetKey<T>(T entity);
        TDbEntity Find(params object[] Ids);
        Task<TDbEntity> FindAsync(params object[] Ids);
        Task<TDbEntity> FirstOrDefaultAsync(Expression<Func<TDbEntity, bool>> Predicate, CancellationToken cancellationToken = new CancellationToken());
    }
}
