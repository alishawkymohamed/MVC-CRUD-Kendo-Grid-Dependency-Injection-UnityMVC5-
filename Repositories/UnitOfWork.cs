using DbContext;
using IRepositories;
using IRepositories.IRepositories;
using Models.DbModels;
using Models.DTOs;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext _Context;
        private Dictionary<string, object> repositories;

        public UnitOfWork(MainContext MainContext)
        {
            _Context = MainContext;
        }

        public bool IsExisted(CheckUniqueDTO checkUniqueDTO)
        {
            StringBuilder Query = new StringBuilder("SELECT COUNT(*) FROM ");
            Query.Append(checkUniqueDTO.TableName + " WHERE ");
            for (int i = 0; i < checkUniqueDTO.Fields.Length; i++)
            {
                Query.Append(checkUniqueDTO.Fields[i] + " LIKE '" + checkUniqueDTO.Values[i] + "'");
                if (checkUniqueDTO.Fields.Length - 1 != i)
                {
                    Query.Append(" AND ");
                }
            }
            string FinalQuery = Query.ToString();

            using (DbCommand command = _Context.Database.Connection.CreateCommand())
            {
                command.CommandText = FinalQuery;
                _Context.Database.Connection.Open();
                object result = command.ExecuteScalar();
                _Context.Database.Connection.Close();
                return ((int)result) > 0;
            }
        }

        public int Save()
        {
            return _Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _Context.SaveChangesAsync(cancellationToken);
        }

        public _IGenericRepository<TDbEntity> Repository<TDbEntity>() where TDbEntity : _BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            Type typeToInstantiate = typeof(_GenericRepository<TDbEntity>).Assembly.GetExportedTypes()
                .FirstOrDefault(t => t.BaseType == typeof(_GenericRepository<TDbEntity>));

            string type = typeof(TDbEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                object repositoryInstance = Activator.CreateInstance(typeToInstantiate, _Context);
                repositories.Add(type, repositoryInstance);
            }
            return (_IGenericRepository<TDbEntity>)repositories[type];
        }


        //To Prevent Calling Database Entities OutSide Repository
        //public DbSet<TDbEntity> Set<TDbEntity>() where TDbEntity : _BaseEntity
        //{
        //    return this._Context.Set<TDbEntity>();
        //}
    }
}
