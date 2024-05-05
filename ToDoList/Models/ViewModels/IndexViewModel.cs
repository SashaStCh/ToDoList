namespace ToDoList.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<Folder> Folders { get; set; }
    }
}
