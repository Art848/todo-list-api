using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using todo_list.DAL.DTO;
using todo_list.DAL.Entities;
using todo_list.DAL.Interfaces;
using todo_list.DAL.Models;
using todo_list.Services.Interfaces;
using BCrypt.Net;

namespace todo_list.Services.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration; // 🔴 ՍԱ ՊԱՐՏԱԴԻՐ Է
    }

    public List<UserModel> getAllUsers()
    {
        var users = _userRepository.getNonAdminUsers();
        var userModels = new List<UserModel>();

        foreach (var user in users)
        {
            var userModel = new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
            userModels.Add(userModel);
        }

        return userModels;
    }

    public void userRegistration(UserDTO userDto)
    {
        var userExists = _userRepository.getByUsername(userDto.Username);
        if (userExists != null)
        {
            throw new Exception("User with this username already exists");
        }

        var user = new User
        {
            Username = userDto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Email = userDto.Email,
            IsAdmin = false,
            RegisteredDate = DateTime.Now
        };

        _userRepository.userRegistration(user);
    }

    private string GenerateJwtToken(User user)
    {
        // 1. Safety Check: Ensure the configuration object itself isn't null
        if (_configuration == null)
        {
            throw new Exception("Dependency Injection Error: IConfiguration was not properly injected into UserService.");
        }

        // 2. Read the secret using the correct connection string path
        var jwtSecret = _configuration.GetConnectionString("JwtSecret");

        // 3. Diagnostic Check: If it can't find the key, throw a descriptive error
        if (string.IsNullOrEmpty(jwtSecret))
        {
            throw new Exception("Configuration Error: 'JwtSecret' was found as null or empty. Ensure appsettings.json is copied to the project root output.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
        }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string userLogin(LoginDTO loginDto)
    {
        var user = _userRepository.getByUsername(loginDto.Username);
        if (user == null)
        {
            throw new Exception("Invalid username or password");
        }

        bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
        if (!isValidPassword)
        {
            throw new Exception("Invalid username or password");
        }

        return GenerateJwtToken(user);
    }

}
