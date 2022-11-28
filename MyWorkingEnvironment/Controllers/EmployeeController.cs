using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWorkingEnvironment.Data;
using MyWorkingEnvironment.Models;
using MyWorkingEnvironment.Repository;
using System.Data;

namespace MyWorkingEnvironment.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class EmployeeController : Controller
    {
        private EmployeeRepository _employeeRepository;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            _employeeRepository = new EmployeeRepository(dbContext);
        }

        // GET: EmployeeController
        [Authorize(Roles = "User, Admin")]
        public ActionResult Index()
        {
            return View(_employeeRepository.GetAllEmployees());
        }

        // GET: EmployeeController/Details/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Details(Guid id)
        {
            return View("DetailsEmployee", _employeeRepository.GetEmployeeById(id));
        }

        // GET: EmployeeController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View("CreateEmployee");
        }

        // POST: EmployeeController/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new EmployeeModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _employeeRepository.InsertEmployee(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateEmployee");
            }
        }

        // GET: EmployeeController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            return View("EditEmployee", _employeeRepository.GetEmployeeById(id));
        }

        // POST: EmployeeController/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new EmployeeModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _employeeRepository.UpdateEmployee(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: EmployeeController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            return View("DeleteEmployee", _employeeRepository.GetEmployeeById(id));
        }

        // POST: EmployeeController/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _employeeRepository.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
