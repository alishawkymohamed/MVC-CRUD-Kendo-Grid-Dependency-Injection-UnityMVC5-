using IRepositories.IRepositories;
using Models.DbModels;
using Models.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IUnitOfWork
    {
        bool IsExisted(CheckUniqueDTO checkUniqueDTO);
        int Save();
        _IGenericRepository<TDbEntity> Repository<TDbEntity>() where TDbEntity : _BaseEntity;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
