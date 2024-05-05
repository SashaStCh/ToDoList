using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index(int folderId)
        {
            var tasks = db.Task.Where(a => a.FolderId == folderId);
            var folders = db.Folder.ToList();
            IndexViewModel viewModel = new IndexViewModel
            {
                Tasks = tasks,
                Folders = folders
            };
            return View(viewModel);
        }
    }
}
