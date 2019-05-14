using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRUDDatabaseTutorial.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;


namespace CRUDDatabaseTutorial.Controllers
{
    public class EmployeeController : Controller
    {
        public List<Division> Divisions = new List<Division>();

        public ActionResult Index()
        {
            return View(RepositoryEmployee.List());
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                return View(DapperORM.ReturnList<Employee>("EmployeeViewByID", RepositoryEmployee.EditList(id)).FirstOrDefault<Employee>());
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(Employee employee, int DivId)
        {
            RepositoryEmployee.EditExecutor(employee,DivId);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            RepositoryEmployee.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Download()
        {
            var fileContent = RepositoryEmployee.Download();
            return File(fileContent, "text/plain", "Data.txt");
        }
    }
}