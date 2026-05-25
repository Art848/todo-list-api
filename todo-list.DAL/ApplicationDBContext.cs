using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using todo_list.DAL.Entities;

namespace todo_list.DAL;

public class ApplicationDBContext : DbContext
{
    private readonly IConfiguration _configuration;
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
        if (!optionsBuilder.IsConfigured)
        {
            // Build the configuration pipeline to read appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            // Fetch the connection string
            string? connectionString = configuration.GetConnectionString("TodoDb");

            // Use it
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
