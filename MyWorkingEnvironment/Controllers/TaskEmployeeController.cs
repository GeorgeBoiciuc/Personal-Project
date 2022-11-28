using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWorkingEnvironment.Data;
using MyWorkingEnvironment.Models;
using MyWorkingEnvironment.Repository;
using System.Data;

namespace MyWorkingEnvironment.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class TaskEmployeeController : Controller
    {
        private EmployeeRepository _employeeRepository;
        private TaskEmployeeRepository _taskEmployeeRepository;

        public TaskEmployeeController(ApplicationDbContext dbContext)
        {
            _employeeRepository = new EmployeeRepository(dbContext);
            _taskEmployeeRepository = new TaskEmployeeRepository(dbContext);
        }

        // GET: TaskEmployeeController
        [Authorize(Roles = "User, Admin")]
        public ActionResult Index()
        {
            return View(_taskEmployeeRepository.GetAllTaskEmployees());
        }

        // GET: TaskEmployeeController/Details/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Details(Guid id)
        {
            return View("DetailsTaskEmployee", _taskEmployeeRepository.GetTaskEmployeeById(id));
        }

        // GET: TaskEmployeeController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            SetUpEmployeeListViewBag();
            return View("CreateTaskEmployee");
        }

        // POST: TaskEmployeeController/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new TaskEmployeeModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _taskEmployeeRepository.InsertTaskEmployee(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateTaskEmployee");
            }
        }

        // GET: TaskEmployeeController/Edit/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Edit(Guid id)
        {
            SetUpEmployeeListViewBag();
            return View("EditTaskEmployee", _taskEmployeeRepository.GetTaskEmployeeById(id));
        }

        // POST: TaskEmployeeController/Edit/5
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new TaskEmployeeModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _taskEmployeeRepository.UpdateTaskEmployee(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: TaskEmployeeController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            return View("DeleteTaskEmployee", _taskEmployeeRepository.GetTaskEmployeeById(id));
        }

        // POST: TaskEmployeeController/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _taskEmployeeRepository.DeleteTaskEmployee(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }

        private void SetUpEmployeeListViewBag()
        {
            var employees = _employeeRepository.GetAllEmployees();
            var employeeList = employees.Select(x => new SelectListItem(x.FirstName + " " + x.LastName, x.IdEmployee.ToString()));
            ViewBag.EmployeeList = employeeList;
        }
    }
}
