using LibraryService.BusinessLogic;
using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Commons;
using LibraryService.Commons.Interfaces;
using LibraryService.Entities;
using LibraryService.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Allow Angular Localhost, If Blocked By Cors Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularLocalhost",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();                    
                    });
            });

            // Config JWT
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

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
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    // Custom Response For Required Input Data From ApiController
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        // Get first error message
                        var firstErrorMessage = context.ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .FirstOrDefault();

                        // Create response
                        string message = firstErrorMessage ?? "Invalid input";
                        var response = new ResponseModel(StatusCodes.Status400BadRequest, message);

                        return new BadRequestObjectResult(response);
                    };
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Register for SQL Server connection
            builder.Services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

            // Register class for dependency injection
            builder.Services.AddScoped<IStaffLogic, StaffLogic>();
            builder.Services.AddScoped<IMembersLogic, MembersLogic>();
            builder.Services.AddScoped<IBooksLogic, BooksLogic>();
            builder.Services.AddScoped<ICategoriesLogic, CategoriesLogic>();
            builder.Services.AddScoped<IBorrowingsLogic, BorrowingsLogic>();
            builder.Services.AddScoped<IJwtService, JwtService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseCors("AllowAngularLocalhost");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
