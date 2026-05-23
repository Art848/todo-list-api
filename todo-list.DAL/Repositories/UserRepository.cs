using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using todo_list.DAL.DTO;
using todo_list.DAL.Entities;
using todo_list.DAL.Interfaces;
using todo_list.DAL.Models;

namespace todo_list.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDBContext _dbContext;
    private readonly string _jwtSecret = "YOUR_SUPER_SECRET_KEY_THAT_IS_LONG_ENOUGH_32_BYTES";

    public UserRepository(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void userRegistration(UserDTO userDto)
    {
        bool userExists = _dbContext.Users.Any(u => u.Username == userDto.Username);
        if (userExists)
        {
            throw new Exception("User with this username already exists");
        }

        var user = new User
        {
            Username = userDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Email = userDto.Email,
            IsAdmin = false,
            RegisteredDate = DateTime.Now,
        };

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public string userLogin(LoginDTO loginDto)
    {
        // Optimized: Find the specific user directly in the database
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == loginDto.Username);

        if (user == null)
        {
            throw new Exception("Invalid username or password");
        }

        // Verify the password using BCrypt
        bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);

        if (!isValidPassword)
        {
            throw new Exception("Invalid username or password");
        }

        // Credentials are good! Generate and return the JWT token
        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User") // Automatically passes roles to your system
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Token valid for 7 days
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public List<UserModel> getAllUsers()
    {
            var users = _dbContext.Users.Where(u => u.IsAdmin == false).ToList();

            var usermodels = new List<UserModel>();

            foreach (var user in users)
            {
                var usermodel = new UserModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email
                };

                usermodels.Add(usermodel);
            }

            return usermodels;
    }
}
