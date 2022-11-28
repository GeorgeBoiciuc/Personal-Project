using MyWorkingEnvironment.Data;
using MyWorkingEnvironment.Models.DBObjects;
using MyWorkingEnvironment.Models;

namespace MyWorkingEnvironment.Repository
{
    public class ClockingRepository
    {
        private ApplicationDbContext _dbContext;

        public ClockingRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public ClockingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ClockingModel MapDBObjectToModel(Clocking dbobject)
        {
            var model = new ClockingModel();
            if (dbobject != null)
            {
                model.IdClocking = dbobject.IdClocking;
                model.IdEmployee = dbobject.IdEmployee;
                model.Type = dbobject.Type;
                model.Date = dbobject.Date;
                model.In = dbobject.In;
                model.Out = dbobject.Out;
            }
            return model;
        }

        private Clocking MapModelToDBObject(ClockingModel model)
        {
            var dbobject = new Clocking();
            if (model != null)
            {
                dbobject.IdClocking = model.IdClocking;
                dbobject.IdEmployee = model.IdEmployee;
                dbobject.Type = model.Type;
                dbobject.Date = model.Date;
                dbobject.In = model.In;
                dbobject.Out = model.Out;
            }
            return dbobject;
        }

        public List<ClockingModel> GetAllClockings()
        {
            var list = new List<ClockingModel>();
            foreach (var dbObject in _dbContext.Clockings)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }

        public ClockingModel GetClokingById(Guid id)
        {
            return MapDBObjectToModel(_dbContext.Clockings.FirstOrDefault(x => x.IdClocking == id));
        }

        public List<ClockingModel> GetAllClokingsByEmployeeId(Guid id)
        {
            var dbObjectsList = _dbContext.Clockings.Where(x => x.IdEmployee == id).ToList();
            var modelList = new List<ClockingModel>();
            foreach (var dbObject in dbObjectsList)
            {
                modelList.Add(MapDBObjectToModel(dbObject));
            }
            return modelList;
        }

        public void InsertClocking(ClockingModel model)
        {
            model.IdClocking = Guid.NewGuid();
            _dbContext.Clockings.Add(MapModelToDBObject(model));
            _dbContext.SaveChanges();
        }

        public void UpdateCloking(ClockingModel model)
        {
            var dbObject = _dbContext.Clockings.FirstOrDefault(x => x.IdClocking == model.IdClocking);
            if (dbObject != null)
            {
                dbObject.IdClocking = model.IdClocking;
                dbObject.IdEmployee = model.IdEmployee;
                dbObject.Type = model.Type;
                dbObject.Date = model.Date;
                dbObject.In = model.In;
                dbObject.Out = model.Out;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteClocking(Guid id)
        {
            var dbObject = _dbContext.Clockings.FirstOrDefault(x => x.IdClocking == id);
            if (dbObject != null)
            {
                _dbContext.Clockings.Remove(dbObject);
                _dbContext.SaveChanges();
            }
        }
    }
}
