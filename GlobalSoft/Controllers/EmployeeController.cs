using IBusinessSevices;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GlobalSoft.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Employees_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(employeeService.GetAll<Employee>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Employees_Create([DataSourceRequest]DataSourceRequest request, Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeService.Insert(new List<Employee>() { emp });
                    return Json(new[] { emp }.ToDataSourceResult(request, ModelState));
                }
                else
                {
                    return Json(new[] { emp }.ToDataSourceResult(request, ModelState));
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }

        [HttpPost]
        public ActionResult Employees_Update([DataSourceRequest] DataSourceRequest request, Employee emp)
        {
            if (emp != null && ModelState.IsValid)
            {
                employeeService.Update(new List<Employee>() { emp });
            }

            return Json(new[] { emp }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Employees_Delete([DataSourceRequest] DataSourceRequest request, Employee emp)
        {
            if (emp != null)
            {
                employeeService.Delete(new List<object>() { emp.EmployeeId });
            }

            return Json(new[] { emp }.ToDataSourceResult(request, ModelState));
        }
    }
}