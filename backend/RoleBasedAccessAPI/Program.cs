using Microsoft.EntityFrameworkCore;
using RoleBasedAccessAPI.Data;
using RoleBasedAccessAPI.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RoleBasedAccessAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load Configuration
            var configuration = builder.Configuration;
            builder.Services.AddControllers();

            // ✅ Add Database Context with MySQL Connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
                )
            );

            // ✅ Register Repositories
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<AuditRepository>();
            builder.Services.AddScoped<ProjectRepository>();

            // ✅ Enable Session Storage
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true;  // Secure cookies
                options.Cookie.IsEssential = true;
            });

            // ✅ Add JWT Authentication
            var jwtKey = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"] ?? "your-super-secret-key");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"] ?? "yourdomain.com",
                        ValidAudience = configuration["JwtSettings:Audience"] ?? "yourdomain.com",
                        IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
                    };
                });

            // ✅ Add Authorization
            builder.Services.AddAuthorization();

            // ✅ Add Swagger for API Documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ✅ Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();

            // ✅ Enable Middleware Order for Sessions, Authentication, and Authorization
            app.UseSession();  // Enable session tracking before authentication
            app.UseAuthentication();
            app.UseAuthorization();

            // ✅ Register Controllers
            app.MapControllers();

            app.Run();
        }
    }
}

#region OLD CODE


//using Microsoft.EntityFrameworkCore;
//using RoleBasedAccessAPI.Data;
//using RoleBasedAccessAPI.Data.Repository;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//namespace RoleBasedAccessAPI
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Load Configuration
//            var configuration = builder.Configuration;
//            builder.Services.AddControllers();

//            // Add Database Context with MySQL Connection
//            builder.Services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseMySql(
//                    configuration.GetConnectionString("DefaultConnection"),
//                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
//                )
//            );

//            // Register Repositories
//            builder.Services.AddScoped<UserRepository>();

//            // Add JWT Authentication
//            var key = Encoding.UTF8.GetBytes("your-super-secret-key");

//            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuer = true,
//                        ValidateAudience = true,
//                        ValidateLifetime = true,
//                        ValidateIssuerSigningKey = true,
//                        ValidIssuer = "yourdomain.com",
//                        ValidAudience = "yourdomain.com",
//                        IssuerSigningKey = new SymmetricSecurityKey(key)
//                    };
//                });

//            // Add Authorization
//            builder.Services.AddAuthorization();

//            // Add Swagger for API Documentation
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();

//            var app = builder.Build();

//            // Configure the HTTP request pipeline
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            // app.UseHttpsRedirection();

//            // Enable Authentication and Authorization
//            app.UseAuthentication();
//            app.UseAuthorization();

//            // Register Controllers
//            app.MapControllers();

//            app.Run();
//        }
//    }
//}
#endregion