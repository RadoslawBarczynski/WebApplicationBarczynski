using WebApplicationBarczynski.Models;

namespace WebApplicationBarczynski.Repositories
{
    public interface ITasksRepository
    {
        Tasks Get(int id);
        IQueryable<Tasks> GetAll();
        IQueryable<Tasks> GetTasksByDateRange(DateTime startDate, DateTime endDate);
        void Add(Tasks task);
        void Update(int id, string description, DateTime date);
        void DoneSelection(int id, bool completedP);
        void Delete(int id);
    }
}
