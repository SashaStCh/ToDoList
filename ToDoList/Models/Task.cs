namespace ToDoList.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int FolderId { get; set; }
        public Folder Folder { get; set; }
        public Task()
        {
            FolderId = 0;
        }
    }
}
