using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Task> Task { get; set; } = null!;
        public DbSet<Folder> Folder { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
