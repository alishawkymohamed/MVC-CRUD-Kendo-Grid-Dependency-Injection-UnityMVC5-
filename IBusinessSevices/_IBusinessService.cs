using Models.DbModels;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace IBusinessSevices
{
    public interface _IBusinessService
    {
    }

    public interface _IBusinessService<TDbEntity> : _IBusinessService
        where TDbEntity : _BaseEntity
    {
        IQueryable<T> GetAll<T>(bool WithTracking = true);
        TDbEntity GetDetails(object Id, bool WithTracking = true);
        IEnumerable<TDbEntity> Insert(IEnumerable<TDbEntity> entities);
        IEnumerable<object> Delete(IEnumerable<object> Ids);
        IEnumerable<TDbEntity> Update(IEnumerable<TDbEntity> Entities);
        bool CheckIfExist(CheckUniqueDTO checkUniqueDTO);
    }
}
