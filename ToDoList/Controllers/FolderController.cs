using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;


namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    public class FolderController : Controller
    {
        ApplicationContext db;

        public FolderController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IEnumerable<Folder> Get()
        {
            return db.Folder.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var folder = db.Folder.FirstOrDefault(a => a.Id == id);
            if (folder == null)
            {
                return NotFound();
            }

            return Ok(folder);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            db.Folder.Remove(db.Folder.FirstOrDefault(a => a.Id == id));
            db.SaveChanges();
            return Ok();
        }

        private int NextFolderID => db.Folder.Count() == 0 ? 1 : db.Folder.Max(a => a.Id) + 1;

        [HttpGet("GetNextFolderId")]
        public int GetNextFolderId()
        {
            Console.WriteLine(NextFolderID);
            return NextFolderID;
        }

        [HttpPost]
        public IActionResult Post(Folder folder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            folder.Id = NextFolderID;
            db.Folder.Add(folder);
            db.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = folder.Id }, folder);
        }

        [HttpPost("CreateFolder")]
        public IActionResult PostBody([FromBody]Folder folder)
        {
            return Post(folder);
        }

        [HttpPut]
        public IActionResult Put(Folder folder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var storedFolder = db.Folder.FirstOrDefault(a => a.Id == folder.Id);
            if (storedFolder == null) return NotFound();
            storedFolder.Name = folder.Name;
            db.SaveChanges();
            return Ok(storedFolder);
        }
        [HttpPut("UpdateFolder")]
        public IActionResult PutBody([FromBody] Folder folder)
        {
            return Put(folder);
        }
    }
}
