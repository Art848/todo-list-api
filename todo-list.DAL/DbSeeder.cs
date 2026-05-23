using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using todo_list.DAL;
using todo_list.DAL.Entities;

public static class DbSeeder
{
    public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

        await db.Database.MigrateAsync();

        // 🛑 RUN ONLY ONCE
        if (await db.Users.AnyAsync())
            return;

        var admin = new User
        {
            Username = "admin",
            Email = "admin@mail.com",
            Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
            IsAdmin = true,
            RegisteredDate = new DateTime(2024, 1, 1),
        };

        db.Users.Add(admin);
        await db.SaveChangesAsync();
    }
}