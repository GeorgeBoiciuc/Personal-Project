using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWorkingEnvironment.Data;
using MyWorkingEnvironment.Models;
using MyWorkingEnvironment.Repository;
using System.Data;

namespace MyWorkingEnvironment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MeetingRoomController : Controller
    {
        private MeetingRoomRepository _meetingRoomRepository;

        public MeetingRoomController(ApplicationDbContext dbContext)
        {
            _meetingRoomRepository = new MeetingRoomRepository(dbContext);
        }

        // GET: MeetingRoomController
        public ActionResult Index()
        {
            return View(_meetingRoomRepository.GetAllMeetingRooms());
        }

        // GET: MeetingRoomController/Details/5
        public ActionResult Details(Guid id)
        {
            return View("DetailsMeetingRoom", _meetingRoomRepository.GetMeetingRoomById(id));
        }

        // GET: MeetingRoomController/Create
        public ActionResult Create()
        {
            return View("CreateMeetingRoom");
        }

        // POST: MeetingRoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new MeetingRoomModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _meetingRoomRepository.InsertMeetingRoom(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateMeetingRoom");
            }
        }

        // GET: MeetingRoomController/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View("EditMeetingRoom", _meetingRoomRepository.GetMeetingRoomById(id));
        }

        // POST: MeetingRoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new MeetingRoomModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _meetingRoomRepository.UpdateMeetingRoom(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }
        }

        // GET: MeetingRoomController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View("DeleteMeetingRoom", _meetingRoomRepository.GetMeetingRoomById(id));
        }

        // POST: MeetingRoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _meetingRoomRepository.DeleteMeetingRoom(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete", id);
            }
        }
    }
}
