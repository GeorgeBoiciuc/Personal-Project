using MyWorkingEnvironment.Data;
using MyWorkingEnvironment.Models.DBObjects;
using MyWorkingEnvironment.Models;

namespace MyWorkingEnvironment.Repository
{
    public class TaskEmployeeRepository
    {
        private ApplicationDbContext _dbContext;

        public TaskEmployeeRepository()
        {
            _dbContext = new ApplicationDbContext();
        }

        public TaskEmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private TaskEmployeeModel MapDBObjectToModel(TaskEmployee dbObject)
        {
            var model = new TaskEmployeeModel();
            if (dbObject != null)
            {
                model.IdTaskEmployee = dbObject.IdTaskEmployee;
                model.IdEmployee = dbObject.IdEmployee;
                model.Title = dbObject.Title;
                model.Priority = dbObject.Priority;
                model.Description = dbObject.Description;
            }
            return model;
        }

        private TaskEmployee MapModelToDBObject(TaskEmployeeModel model)
        {
            var dbObject = new TaskEmployee();
            if (model != null)
            {
                dbObject.IdTaskEmployee = model.IdTaskEmployee;
                dbObject.IdEmployee = model.IdEmployee;
                dbObject.Title = model.Title;
                dbObject.Priority = model.Priority;
                dbObject.Description = model.Description;
            }
            return dbObject;
        }

        public List<TaskEmployeeModel> GetAllTaskEmployees()
        {
            var list = new List<TaskEmployeeModel>();
            foreach (var dbObject in _dbContext.TaskEmployees)
            {
                list.Add(MapDBObjectToModel(dbObject));
            }
            return list;
        }

        public TaskEmployeeModel GetTaskEmployeeById(Guid id)
        {
            return MapDBObjectToModel(_dbContext.TaskEmployees.FirstOrDefault(x => x.IdTaskEmployee == id));
        }

        public void InsertTaskEmployee(TaskEmployeeModel model)
        {
            model.IdTaskEmployee = Guid.NewGuid();
            _dbContext.TaskEmployees.Add(MapModelToDBObject(model));
            _dbContext.SaveChanges();
        }

        public void UpdateTaskEmployee(TaskEmployeeModel model)
        {
            var dbObject = _dbContext.TaskEmployees.FirstOrDefault(x => x.IdTaskEmployee == model.IdTaskEmployee);
            if (dbObject != null)
            {
                dbObject.IdTaskEmployee = model.IdTaskEmployee;
                dbObject.IdEmployee = model.IdEmployee;
                dbObject.Title = model.Title;
                dbObject.Priority = model.Priority;
                dbObject.Description = model.Description;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteTaskEmployee(Guid id)
        {
            var dbObject = _dbContext.TaskEmployees.FirstOrDefault(x => x.IdTaskEmployee == id);
            if (dbObject != null)
            {
                _dbContext.TaskEmployees.Remove(dbObject);
                _dbContext.SaveChanges();
            }
        }
    }
}
