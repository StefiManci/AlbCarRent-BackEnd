using AlbCarRent.Datalayer;
using AlbCarRent.Modules.AdminModule.Application.Interfaces;
using AlbCarRent.Modules.AdminModule.Application.Services;
using AlbCarRent.Modules.AdminModule.Domain;
using AlbCarRent.Modules.AdminModule.Infrastructure;
using AlbCarRent.Modules.AuthModule.Application.Interfaces;
using AlbCarRent.Modules.AuthModule.Application.Services;
using AlbCarRent.Modules.AuthModule.Domain;
using AlbCarRent.Modules.AuthModule.Infrastructure;
using AlbCarRent.Modules.Booking.Application.Interfaces;
using AlbCarRent.Modules.Booking.Application.Services;
using AlbCarRent.Modules.Booking.Domain;
using AlbCarRent.Modules.Booking.Infrastructure;
using AlbCarRent.Modules.BusinessModule.Application.Interfaces;
using AlbCarRent.Modules.BusinessModule.Application.Services;
using AlbCarRent.Modules.BusinessModule.Domain;
using AlbCarRent.Modules.BusinessModule.Infrastructure;
using AlbCarRent.Modules.CarModule.Application.Interfaces;
using AlbCarRent.Modules.CarModule.Application.Services;
using AlbCarRent.Modules.CarModule.Domain;
using AlbCarRent.Modules.CarModule.Infrastructure;
using AlbCarRent.Modules.CustomerModule.Application.Interfaces;
using AlbCarRent.Modules.CustomerModule.Application.Services;
using AlbCarRent.Modules.CustomerModule.Domain;
using AlbCarRent.Modules.CustomerModule.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding Services

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IBusinessRepository,BusinessRepository>();
builder.Services.AddScoped<IBusinessService,BusinessService>();
builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICarRepository,CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IBookingService,BookingService>();
builder.Services.AddScoped<IBookingRepository,BookingRepository>();

//Configure EF COre with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Configure Identity

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Client", policy =>
       policy.RequireRole("Client"));

    options.AddPolicy("Bussiness", policy =>
      policy.RequireRole("Bussiness"));
});

// Define CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()    
              .AllowAnyMethod()    
              .AllowCredentials(); 
    });
});

var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
