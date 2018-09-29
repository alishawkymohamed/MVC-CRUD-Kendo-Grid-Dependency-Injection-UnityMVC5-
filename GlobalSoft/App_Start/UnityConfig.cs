using BusinessSevices.cs;
using DbContext;
using IBusinessSevices;
using IRepositories;
using IRepositories.IRepositories;
using Repositories;
using Repositories.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace GlobalSoft
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            UnityContainer container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<MainContext, MainContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<IEmployeeService, EmployeeService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}