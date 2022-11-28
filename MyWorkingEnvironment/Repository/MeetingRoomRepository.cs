using MyWorkingEnvironment.Data;
using MyWorkingEnvironment.Models.DBObjects;
using MyWorkingEnvironment.Models;

namespace MyWorkingEnvironment.Repository
{
    public class MeetingRoomRepository
    {
        private ApplicationDbContext _dbContext;

        public MeetingRoomRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public MeetingRoomRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private MeetingRoomModel MapDBObjectToModel(MeetingRoom dbobject)
        {
            var model = new MeetingRoomModel();
            if (dbobject != null)
            {
                model.IdMeetingRoom = dbobject.IdMeetingRoom;
                model.Name = dbobject.Name;
                model.Floor = dbobject.Floor;
                model.Capacity = dbobject.Capacity;
            }
            return model;
        }

        private MeetingRoom MapModelToDBObject(MeetingRoomModel model)
        {
            var dbobject = new MeetingRoom();
            if (model != null)
            {
                dbobject.IdMeetingRoom = model.IdMeetingRoom;
                dbobject.Name = model.Name;
                dbobject.Floor = model.Floor;
                dbobject.Capacity = model.Capacity;
            }
            return dbobject;
        }

        public List<MeetingRoomModel> GetAllMeetingRooms()
        {
            var list = new List<MeetingRoomModel>();
            foreach (var dbObject in _dbContext.MeetingRooms)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }

        public MeetingRoomModel GetMeetingRoomById(Guid id)
        {
            return MapDBObjectToModel(_dbContext.MeetingRooms.FirstOrDefault(x => x.IdMeetingRoom == id));
        }

        public void InsertMeetingRoom(MeetingRoomModel model)
        {
            model.IdMeetingRoom = Guid.NewGuid();
            _dbContext.MeetingRooms.Add(MapModelToDBObject(model));
            _dbContext.SaveChanges();
        }

        public void UpdateMeetingRoom(MeetingRoomModel model)
        {
            var dbObject = _dbContext.MeetingRooms.FirstOrDefault(x => x.IdMeetingRoom == model.IdMeetingRoom);
            if (dbObject != null)
            {
                dbObject.IdMeetingRoom = model.IdMeetingRoom;
                dbObject.Name = model.Name;
                dbObject.Floor = model.Floor;
                dbObject.Capacity = model.Capacity;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteMeetingRoom(Guid id)
        {
            var dbObject = _dbContext.MeetingRooms.FirstOrDefault(x => x.IdMeetingRoom == id);
            if (dbObject != null)
            {
                _dbContext.MeetingRooms.Remove(dbObject);
                _dbContext.SaveChanges();
            }
        }
    }
}
