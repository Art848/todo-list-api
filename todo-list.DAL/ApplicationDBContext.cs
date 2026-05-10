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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            Email = "admin@mail.com",
            Password = "admin123",
            isLogged = false,
            IsAdmin = true,
            RegisteredDate = new DateTime(2024, 1, 1),
        });
    }
}
