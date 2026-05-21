using Microsoft.EntityFrameworkCore;
using todo_list.DAL.Entities;

namespace todo_list.DAL;

public class ApplicationDBContext : DbContext
{
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDBContext()
    {
        
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=todo-list");
    }
}
