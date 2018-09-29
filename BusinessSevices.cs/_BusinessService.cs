using AutoMapper;
using IBusinessSevices;
using IRepositories;
using Models.DbModels;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessSevices.cs
{
    public abstract class _BusinessService : _IBusinessService
    {
        protected readonly IUnitOfWork _UnitOfWork;
        //protected readonly IMapper _Mapper;

        public _BusinessService(IUnitOfWork unitOfWork/*, IMapper mapper*/)
        {
            _UnitOfWork = unitOfWork;
            //_Mapper = mapper;
        }

    }

    public abstract class _BusinessService<TDbEntity> : _BusinessService, _IBusinessService<TDbEntity>
             where TDbEntity : _BaseEntity
    {
        public _BusinessService(IUnitOfWork unitOfWork/*, IMapper mapper*/) : base(unitOfWork/*, mapper*/) { }

        public virtual IQueryable<T> GetAll<T>(bool WithTracking = true)
        {
            return _UnitOfWork.Repository<TDbEntity>().GetAll(WithTracking).Cast<T>();
        }

        public virtual TDbEntity GetDetails(object Id, bool WithTracking = true)
        {
            if (Id == null)
            {
                return null;
            }
            return _UnitOfWork.Repository<TDbEntity>().GetById(Id);
        }

        public virtual IEnumerable<TDbEntity> Insert(IEnumerable<TDbEntity> entities)
        {
            return _UnitOfWork.Repository<TDbEntity>().Insert(entities);
        }

        public virtual IEnumerable<object> Delete(IEnumerable<object> Ids)
        {
            int RecordDeleted;
            IEnumerable<object> DeletedRecords = _UnitOfWork.Repository<TDbEntity>().Delete(Ids);
            try
            {
                RecordDeleted = _UnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RecordDeleted == Ids.Count() ? DeletedRecords : null;
        }

        public virtual IEnumerable<TDbEntity> Update(IEnumerable<TDbEntity> Entities)
        {
            int RecordsUpdated;
            foreach (TDbEntity Entity in Entities)
            {
                //To Copy Data not Sent From and To UI
                object[] PrimaryKeysValues = _UnitOfWork.Repository<TDbEntity>().GetKey<TDbEntity>(Entity);
                TDbEntity OldEntity = _UnitOfWork.Repository<TDbEntity>().Find(PrimaryKeysValues);
                object MappedEntity = Mapper.Map(Entity, OldEntity, typeof(TDbEntity), typeof(TDbEntity));
                _UnitOfWork.Repository<TDbEntity>().Update(MappedEntity as TDbEntity);
            }
            try
            {
                RecordsUpdated = _UnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RecordsUpdated == Entities.Count() ? Entities : null;
        }

        public bool CheckIfExist(CheckUniqueDTO checkUniqueDTO)
        {
            return _UnitOfWork.IsExisted(checkUniqueDTO);
        }
    }
}
