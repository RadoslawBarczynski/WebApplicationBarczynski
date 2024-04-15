using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplicationBarczynski.Models;
using WebApplicationBarczynski.Repositories;

namespace WebApplicationBarczynski.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksRepository _tasksRepository;

        public TasksController(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public IActionResult Index()
        {
            var tasks = _tasksRepository.GetAll();
            return View(tasks);
        }

        [HttpPost]
        public ActionResult Add(string description, DateTime date)
        {
            Tasks task = new Tasks
            {
                Description = description,
                Date = date,
                CompletedP = false
            };

            _tasksRepository.Add(task);
            var tasks = _tasksRepository.GetAll();


            return TaskList(tasks);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _tasksRepository.Delete(id);
            var tasks = _tasksRepository.GetAll();

            return TaskList(tasks);
        }

        [HttpPost]
        public ActionResult Update(int id, string description, DateTime date)
        {
            _tasksRepository.Update(id, description, date);
            var tasks = _tasksRepository.GetAll();

            return TaskList(tasks);
        }

        public ActionResult Filter(DateTime startDate, DateTime endDate)
        {
            var tasks = _tasksRepository.GetTasksByDateRange(startDate, endDate);

            return TaskList(tasks);
        }

        public ActionResult TaskList(IQueryable<Tasks> tasks)
        {
            return PartialView("_TaskList", tasks);
        }

        [HttpPost]
        public ActionResult DoneSelection(int id, bool completedP)
        {
            _tasksRepository.DoneSelection(id, completedP);

            return new EmptyResult();
        }

        public ActionResult ClearFilters()
        {
            var tasks = _tasksRepository.GetAll();

            return PartialView("_TaskList", tasks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}