using System.Text;
using todo_list.DAL;
using todo_list.DAL.Interfaces;
using todo_list.DAL.Repositories;
using todo_list.Services.Interfaces;
using todo_list.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDBContext>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITaskItemRepository, TaskItemRepository>();
builder.Services.AddTransient<ITaskItemService, TaskItemService>();

var jwtSecret = "YOUR_SUPER_SECRET_KEY_THAT_IS_LONG_ENOUGH_32_BYTES"; // Պետք է նույնը լինի, ինչ Repository-ում է
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,   // Փոքր պրոյեկտների համար կարող ես սրանք false թողնել
        ValidateAudience = false
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

await DbSeeder.SeedAdminAsync(app.Services);



app.UseHttpsRedirection();

app.UseCors("AllowAll");


app.UseAuthentication(); // ՍԱ ՄԻՇՏ ԱՌԱՋԻՆԸ
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();
app.Run();
