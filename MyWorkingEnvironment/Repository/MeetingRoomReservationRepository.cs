using MyWorkingEnvironment.Data;
using MyWorkingEnvironment.Models;
using MyWorkingEnvironment.Models.DBObjects;

namespace MyWorkingEnvironment.Repository
{
    public class MeetingRoomReservationRepository
    {
        private ApplicationDbContext _dbContext;

        public MeetingRoomReservationRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public MeetingRoomReservationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private MeetingRoomReservationModel MapDBObjectToModel(MeetingRoomReservation dbobject)
        {
            var model = new MeetingRoomReservationModel();
            if (dbobject != null)
            {
                model.IdMeetingRoomReservation = dbobject.IdMeetingRoomReservation;
                model.IdReservation = dbobject.IdReservation;
                model.IdMeetingRoom = dbobject.IdMeetingRoom;
            }
            return model;
        }

        private MeetingRoomReservation MapModelToDBObject(MeetingRoomReservationModel model)
        {
            var dbobject = new MeetingRoomReservation();
            if (model != null)
            {
                dbobject.IdMeetingRoomReservation = model.IdMeetingRoomReservation;
                dbobject.IdReservation = model.IdReservation;
                dbobject.IdMeetingRoom = model.IdMeetingRoom;
            }
            return dbobject;
        }

        public List<MeetingRoomReservationModel> GetAllMeetingRoomReservations()
        {
            var list = new List<MeetingRoomReservationModel>();
            foreach (var dboject in _dbContext.MeetingRoomReservations)
            {
                list.Add(MapDBObjectToModel(dboject));
            }
            return list;
        }

        public MeetingRoomReservationModel GetMeetingRoomReservationById(Guid id)
        {
            return MapDBObjectToModel(_dbContext.MeetingRoomReservations.FirstOrDefault(x => x.IdMeetingRoomReservation == id));
        }

        public MeetingRoomReservationModel GetReservationByMeetingRoomId(Guid id)
        {
            return MapDBObjectToModel(_dbContext.MeetingRoomReservations.FirstOrDefault(x => x.IdMeetingRoom == id));
        }

        public MeetingRoomReservationModel GetMeetingRoomByReservationId(Guid id)
        {
            return MapDBObjectToModel(_dbContext.MeetingRoomReservations.FirstOrDefault(x => x.IdReservation == id));
        }

        public void InsertMeetingRoomReservation(MeetingRoomReservationModel model)
        {
            model.IdMeetingRoomReservation = Guid.NewGuid();
            _dbContext.MeetingRoomReservations.Add(MapModelToDBObject(model));
            _dbContext.SaveChanges();
        }

        public void UpdateMeetingRoomReservation(MeetingRoomReservationModel model)
        {
            var dbobject = _dbContext.MeetingRoomReservations.FirstOrDefault(x => x.IdMeetingRoomReservation == model.IdMeetingRoomReservation);
            if (dbobject != null)
            {
                dbobject.IdMeetingRoomReservation = model.IdMeetingRoomReservation;
                dbobject.IdReservation = model.IdReservation;
                dbobject.IdMeetingRoom = model.IdMeetingRoom;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteMeetingRoomReservation(Guid id)
        {
            var dbobject = _dbContext.MeetingRoomReservations.FirstOrDefault(x => x.IdMeetingRoomReservation == id);
            if (dbobject != null)
            {
                _dbContext.MeetingRoomReservations.Remove(dbobject);
                _dbContext.SaveChanges();
            }
        }
    }
}
