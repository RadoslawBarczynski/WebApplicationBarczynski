using WebApplicationBarczynski.Data;
using WebApplicationBarczynski.Models;

namespace WebApplicationBarczynski.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly AppDbContext _context;

        public TasksRepository(AppDbContext context)
        {
            _context = context;
        }

        public Tasks Get(int id)
        {
            Tasks task = _context.Tasks.SingleOrDefault(x => x.Id.Equals(id));
            return task;
        }

        public IQueryable<Tasks> GetAll() => _context.Tasks;

        public IQueryable<Tasks> GetTasksByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Tasks.Where(task => task.Date >= startDate && task.Date <= endDate);
        }

        public void Add(Tasks task)
        {
            _context.Tasks.Add(task);

            _context.SaveChanges();
        }


        public void Update(int id, string description, DateTime date)
        {
            var result = _context.Tasks.FirstOrDefault(x => x.Id.Equals(id));

            if (result != null)
            {
                result.Description = description;
                result.Date = date;
            }

            _context.SaveChanges();
        }

        public void DoneSelection(int id, bool completedP)
        {
            var result = _context.Tasks.FirstOrDefault(x => x.Id.Equals(id));

            if (result != null)
            {
                result.CompletedP = completedP;
            }

            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            _context.Tasks.Remove(_context.Tasks.FirstOrDefault(x => x.Id.Equals(id)));

            _context.SaveChanges();
        }
    }
}
