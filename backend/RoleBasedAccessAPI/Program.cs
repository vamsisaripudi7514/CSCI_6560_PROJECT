using Microsoft.EntityFrameworkCore;
using RoleBasedAccessAPI.Data;
using RoleBasedAccessAPI.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using RoleBasedAccessAPI.Utility;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace RoleBasedAccessAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load Configuration
            var configuration = builder.Configuration;

            JwtHelper.SecretKey = configuration.GetValue<string>("Jwt:Key") ?? "";
            JwtHelper.Key = Encoding.ASCII.GetBytes(JwtHelper.SecretKey);
            JwtHelper.strIssuer = configuration.GetValue<string>("Jwt:Issuer") ?? "";
            JwtHelper.strAudince = configuration.GetValue<string>("Jwt:Audience") ?? "";

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

            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = JwtHelper.strIssuer,
                        ValidAudience = JwtHelper.strAudince,
                        IssuerSigningKey = new SymmetricSecurityKey(JwtHelper.Key)
                    };
                });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IgetTokenData, getTokenDataFromJWT>();
            // ✅ Add Authorization
            builder.Services.AddAuthorization();

            // ✅ Add Swagger for API Documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JWTToken_Auth_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },

            },
            new List<string>()
          }
                });
                //c.OperationFilter<SecureEndPointAuthRequirementFilter>();
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.AllowAnyOrigin() // Allows requests from any origin
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // ✅ Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();
            app.UseCors("AllowReactApp");
            // ✅ Enable Middleware Order for Sessions, Authentication, and Authorization
            app.UseSession();  // Enable session tracking before authentication
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<JWTMiddleWare>();

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