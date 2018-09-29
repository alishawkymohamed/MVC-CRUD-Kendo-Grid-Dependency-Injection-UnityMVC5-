using DbContext;
using IRepositories.IRepositories;
using Models.DbModels;

namespace Repositories.Repositories
{
    public class EmployeeRepository : _GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MainContext MainContext) : base(MainContext)
        {
        }
    }
}
