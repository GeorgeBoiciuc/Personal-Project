using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWorkingEnvironment.Data;
using MyWorkingEnvironment.Models;
using MyWorkingEnvironment.Repository;
using MyWorkingEnvironment.ViewModels;
using System.Data;

namespace MyWorkingEnvironment.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class MeetingRoomReservationController : Controller
    {
        private ReservationRepository _reservationRepository;
        private MeetingRoomRepository _meetingRoomRepository;
        private MeetingRoomReservationRepository _meetingRoomReservationRepository;

        public MeetingRoomReservationController(ApplicationDbContext dbContext)
        {
            _reservationRepository = new ReservationRepository(dbContext);
            _meetingRoomRepository = new MeetingRoomRepository(dbContext);
            _meetingRoomReservationRepository = new MeetingRoomReservationRepository(dbContext);
        }

        // GET: MeetingRoomReservationController
        [Authorize(Roles = "User, Admin")]
        public ActionResult Index()
        {
            var list = _meetingRoomReservationRepository.GetAllMeetingRoomReservations();
            var viewModelList = new List<MeetingRoomReservationViewModel>();
            foreach (var model in list)
            {
                viewModelList.Add(new MeetingRoomReservationViewModel(model, _reservationRepository, _meetingRoomRepository));
            }
            return View(viewModelList);
        }

        // GET: MeetingRoomReservationController/Details/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Details(Guid id)
        {
            var viewModel = new MeetingRoomReservationViewModel(_meetingRoomReservationRepository.GetMeetingRoomReservationById(id), 
                                                                _reservationRepository, 
                                                                _meetingRoomRepository);
            return View("DetailsMeetingRoomReservation", viewModel);
        }

        // GET: MeetingRoomReservationController/Create
        [Authorize(Roles = "User, Admin")]
        public ActionResult Create()
        {
            var reservations = _reservationRepository.GetAllReservations();
            var reservationList = reservations.Select(x => new SelectListItem(x.Date.ToShortDateString() + " " + x.Start.ToShortTimeString() + "-" + x.End.ToShortTimeString(),
                                                                              x.IdReservation.ToString()));
            ViewBag.ReservationList = reservationList;
            var meetingRooms = _meetingRoomRepository.GetAllMeetingRooms();
            var meetingRoomList = meetingRooms.Select(x => new SelectListItem(x.Name, x.IdMeetingRoom.ToString()));
            ViewBag.MeetingRoomList = meetingRoomList;
            return View("CreateMeetingRoomReservation");
        }

        // POST: MeetingRoomReservationController/Create
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new MeetingRoomReservationModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _meetingRoomReservationRepository.InsertMeetingRoomReservation(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateMeetingRoomReservation");
            }
        }

        // GET: MeetingRoomReservationController/Edit/5
        [Authorize(Roles = "User, Admin")]
        public ActionResult Edit(Guid id)
        {
            var reservations = _reservationRepository.GetAllReservations();
            var reservationList = reservations.Select(x => new SelectListItem(x.Date.ToShortDateString() + " " + x.Start.ToShortTimeString() + "-" + x.End.ToShortTimeString(),
                                                                              x.IdReservation.ToString()));
            ViewBag.ReservationList = reservationList;
            var meetingRooms = _meetingRoomRepository.GetAllMeetingRooms();
            var meetingRoomList = meetingRooms.Select(x => new SelectListItem(x.Name, x.IdMeetingRoom.ToString()));
            ViewBag.MeetingRoomList = meetingRoomList;
            return View("EditMeetingRoomReservation", _meetingRoomReservationRepository.GetMeetingRoomReservationById(id));
        }

        // POST: MeetingRoomReservationController/Edit/5
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new MeetingRoomReservationModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _meetingRoomReservationRepository.UpdateMeetingRoomReservation(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: MeetingRoomReservationController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            return View("DeleteMeetingRoomReservation",_meetingRoomReservationRepository.GetMeetingRoomReservationById(id));
        }

        // POST: MeetingRoomReservationController/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _meetingRoomReservationRepository.DeleteMeetingRoomReservation(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete", id);
            }
        }
    }
}
