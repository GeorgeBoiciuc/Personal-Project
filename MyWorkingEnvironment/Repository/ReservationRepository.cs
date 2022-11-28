using MyWorkingEnvironment.Data;
using MyWorkingEnvironment.Models.DBObjects;
using MyWorkingEnvironment.Models;

namespace MyWorkingEnvironment.Repository
{
    public class ReservationRepository
    {
        private ApplicationDbContext _dbContext;

        public ReservationRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public ReservationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ReservationModel MapDBObjectToModel(Reservation dbobject)
        {
            var model = new ReservationModel();
            if (dbobject != null)
            {
                model.IdReservation = dbobject.IdReservation;
                model.IdEmployee = dbobject.IdEmployee;
                model.Date = dbobject.Date;
                model.Start = dbobject.Start;
                model.End = dbobject.End;
            }
            return model;
        }

        private Reservation MapModelToDBObject(ReservationModel model)
        {
            var dbobject = new Reservation();
            if (model != null)
            {
                dbobject.IdReservation = model.IdReservation;
                dbobject.IdEmployee = model.IdEmployee;
                dbobject.Date = model.Date;
                dbobject.Start = model.Start;
                dbobject.End = model.End;
            }
            return dbobject;
        }

        public List<ReservationModel> GetAllReservations()
        {
            var list = new List<ReservationModel>();
            foreach (var dbObject in _dbContext.Reservations)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }

        public ReservationModel GetReservationById(Guid id)
        {
            return MapDBObjectToModel(_dbContext.Reservations.FirstOrDefault(x => x.IdReservation == id));
        }

        public void InsertReservation(ReservationModel model)
        {
            model.IdReservation = Guid.NewGuid();
            _dbContext.Reservations.Add(MapModelToDBObject(model));
            _dbContext.SaveChanges();
        }

        public void UpdateReservation(ReservationModel model)
        {
            var dbObject = _dbContext.Reservations.FirstOrDefault(x => x.IdReservation == model.IdReservation);
            if (dbObject != null)
            {
                dbObject.IdReservation = model.IdReservation;
                dbObject.IdEmployee = model.IdEmployee;
                dbObject.Date = model.Date;
                dbObject.Start = model.Start;
                dbObject.End = model.End;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteReservation(Guid id)
        {
            var dbObject = _dbContext.Reservations.FirstOrDefault(x => x.IdReservation == id);
            if (dbObject != null)
            {
                _dbContext.Reservations.Remove(dbObject);
                _dbContext.SaveChanges();
            }
        }
    }
}
