using IBusinessSevices;
using IRepositories;
using Models.DbModels;

namespace BusinessSevices.cs
{
    public class EmployeeService : _BusinessService<Employee>, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
