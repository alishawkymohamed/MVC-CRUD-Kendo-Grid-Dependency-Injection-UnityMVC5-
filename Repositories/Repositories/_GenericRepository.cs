using DbContext;
using IRepositories.IRepositories;
using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class _GenericRepository<TDbEntity> : _IGenericRepository<TDbEntity> where TDbEntity : _BaseEntity
    {
        protected readonly MainContext _Context;
        protected readonly DbSet<TDbEntity> _DbSet;

        public _GenericRepository(MainContext MainContext)
        {
            _Context = MainContext;
            _DbSet = _Context.Set<TDbEntity>();
        }

        public virtual IQueryable<TDbEntity> GetAll(bool WithTracking = true)
        {
            if (WithTracking)
            {
                return _DbSet;
            }
            else
            {
                return _DbSet.AsNoTracking();
            }
        }

        public virtual TDbEntity GetById(object Id)
        {
            return Find(Id);
        }

        public IEnumerable<TDbEntity> Insert(IEnumerable<TDbEntity> Entities)
        {
            int RecordsInserted;
            foreach (TDbEntity Entity in Entities)
            {
                _DbSet.Add(Entity);
            }
            try
            {
                RecordsInserted = _Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RecordsInserted == Entities.Count() ? Entities : null;
        }

        public virtual IEnumerable<object> Delete(IEnumerable<object> Ids)
        {
            foreach (object Id in Ids)
            {
                TDbEntity ToBeRemoved = GetById(Id);
                _DbSet.Remove(ToBeRemoved);
            }
            return Ids;
        }

        public virtual void Update(TDbEntity Entity)
        {
            _Context.Entry<TDbEntity>(Entity).State = EntityState.Modified;
        }

        public virtual object[] GetKey<T>(T entity)
        {
            object[] keyNames = GetKeyNames<T>(entity);
            Type type = typeof(T);

            object[] keys = new object[keyNames.Length];
            for (int i = 0; i < keyNames.Length; i++)
            {
                keys[i] = type.GetProperty(keyNames[i].ToString()).GetValue(entity, null);
            }
            return keys;
        }

        public virtual object[] GetKeyNames<T>(T entity)
        {
            ObjectContext objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_Context).ObjectContext;
            ObjectSet<TDbEntity> set = objectContext.CreateObjectSet<TDbEntity>();
            IEnumerable<string> keyNames = set.EntitySet.ElementType.KeyMembers.Select(k => k.Name);
            return keyNames.ToArray<object>();
        }

        public TDbEntity Find(params object[] Ids)
        {
            return _DbSet.Find(Ids);
        }

        public virtual async Task<TDbEntity> FindAsync(params object[] Ids)
        {
            return await _DbSet.FindAsync(Ids);
        }

        public virtual async Task<TDbEntity> FirstOrDefaultAsync(Expression<Func<TDbEntity, bool>> Predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _DbSet.FirstOrDefaultAsync(Predicate, cancellationToken);
        }
    }
}
