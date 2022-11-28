using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWorkingEnvironment.Data;
using MyWorkingEnvironment.Models;
using MyWorkingEnvironment.Repository;
using MyWorkingEnvironment.ViewModels;
using System.Collections.Generic;

namespace MyWorkingEnvironment.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class ClockingController : Controller
    {
        private EmployeeRepository _employeeRepository;
        private ClockingRepository _clockingRepository;

        public ClockingController(ApplicationDbContext dbContext)
        {
            _employeeRepository = new EmployeeRepository(dbContext);
            _clockingRepository = new ClockingRepository(dbContext);
        }

        // GET: ClockingController
        [Authorize(Roles = "User, Admin")]
        public ActionResult Index()
        {
            if (User.IsInRole("User"))
            {
                return View(CreateViewModelList(_clockingRepository.GetAllClokingsByEmployeeId(_employeeRepository.GetEmployeeByEmailAddress(User.Identity.Name).IdEmployee)));
            }
            else
            {
                return View(CreateViewModelList(_clockingRepository.GetAllClockings()));
            }
        }

        // GET: ClockingController/Details/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Details(Guid id)
        {
            return View("DetailsClocking", _clockingRepository.GetClokingById(id));
        }

        // GET: ClockingController/Create
        [Authorize(Roles = "User, Admin")]
        public ActionResult Create()
        {
            var employees = _employeeRepository.GetAllEmployees();
            var employeeList = employees.Select(x => new SelectListItem(x.FirstName + " " + x.LastName, x.IdEmployee.ToString()));
            ViewBag.EmployeeList = employeeList;
            return View("CreateClocking");
        }

        // POST: ClockingController/Create
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new ClockingModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (model.Type == "Vacation" && !CheckIfVacationClockingsAreLessThanVacationDays(model))
                {
                    return View("VacationDaysErrorPage");
                }
                if (task.Result)
                {
                    _clockingRepository.InsertClocking(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateClocking");
            }
        }

        // GET: ClockingController/Edit/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Edit(Guid id)
        {
            return View("EditClocking", _clockingRepository.GetClokingById(id));
        }

        // POST: ClockingController/Edit/5
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new ClockingModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _clockingRepository.UpdateCloking(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: ClockingController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            return View("DeleteClocking", _clockingRepository.GetClokingById(id));
        }

        // POST: ClockingController/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _clockingRepository.DeleteClocking(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }

        private List<ClockingsEmployeeViewModel> CreateViewModelList(List<ClockingModel> list)
        {
            var viewModelList = new List<ClockingsEmployeeViewModel>();
            foreach (var model in list)
            {
                viewModelList.Add(new ClockingsEmployeeViewModel(model, _employeeRepository));
            }
            return viewModelList;
        }

        private bool CheckIfVacationClockingsAreLessThanVacationDays(ClockingModel model)
        {
            return _employeeRepository.GetEmployeeById((Guid)model.IdEmployee).VacationDays >
                _clockingRepository.GetAllClokingsByEmployeeId((Guid)model.IdEmployee).Where(x => x.Type == "Vacation").ToList().Count;
        }
    }
}
