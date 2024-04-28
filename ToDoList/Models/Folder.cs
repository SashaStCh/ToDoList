namespace ToDoList.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Task> Tasks { get; set; } = new();
    }
}
