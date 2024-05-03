using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        ApplicationContext db;

        public TaskController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<Models.Task> Get()
        {
            return db.Task.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var task = db.Task.FirstOrDefault(a => a.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpGet("FromFolder/{id}")]
        public IActionResult GetFromFolder(int id)
        {
            var tasks = db.Task.Where(a => a.FolderId == id).ToList();
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            db.Task.Remove(db.Task.FirstOrDefault(a => a.Id == id));
            db.SaveChanges();
            return Ok();
        }
        private int NextTaskID => db.Task.Count() == 0 ? 1 : db.Task.Max(a => a.Id) + 1;

        [HttpGet("GetNextTaskId")]
        public int GetNextTaskId()
        {
            return NextTaskID;
        }

        [HttpPost]
        public IActionResult Post(Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            task.Id = NextTaskID;
            db.Task.Add(task);
            db.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }

        [HttpPost("CreateTask")]
        public IActionResult PostBody([FromBody] Models.Task task)
        {
            return Post(task);
        }

        [HttpPut]
        public IActionResult Put(Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var storedTask = db.Task.FirstOrDefault(a => a.Id == task.Id);
            if (storedTask == null) return NotFound();
            storedTask.Text = task.Text;
            db.SaveChanges();
            return Ok(storedTask);
        }
        [HttpPut("UpdateTask")]
        public IActionResult PutBody([FromBody] Models.Task task)
        {
            return Put(task);
        }
    }
}
