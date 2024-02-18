using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskmanagementapi.Models;
using Task = taskmanagementapi.Models.Task;

namespace taskmanagementapi.Controllers
{

    [Authorize] // Restricts access to authenticated users
    public class TaskController : ControllerBase
    {

        public TaskDbContext Context { get; }
        public TaskController(TaskDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = Context.tasks.Where(t => t.status==false).OrderBy(x => x.Id);
            return new ObjectResult(tasks);
        }

        [HttpGet]
        public IActionResult GetAllTask()
        {
            var tasks = Context.tasks.OrderBy(x => x.Id);
            return new ObjectResult(tasks);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Task task)
        {
            Context.Update(task);
            Context.SaveChanges();
            return new ObjectResult(task.Id);
        }

        public IActionResult get(int id)
        {
            var task = Context.tasks.Where(x => x.Id == id).SingleOrDefault();
            return new ObjectResult(task);
        }

        public IActionResult delete(Task task)
        {
            var Deletetask = Context.tasks.FirstOrDefault(s => s.Id == task.Id);
            if (Deletetask != null)
            {
                Context.tasks.Remove(Deletetask);
                Context.SaveChanges();
            }

            return new ObjectResult(Deletetask.Id);
        }

        public IActionResult complete(Task task)
        {
            var Completetask = Context.tasks.FirstOrDefault(s => s.Id == task.Id);
            if (Completetask != null)
            {
                Completetask.status = true;
                Context.Update(Completetask);
                Context.SaveChanges();
            }

            return new ObjectResult(Completetask.Id);
        }


    }
}
