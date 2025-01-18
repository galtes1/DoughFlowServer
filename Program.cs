using Microsoft.AspNetCore.Authentication.JwtBearer;
using AccountManagementServer.Application.Interface;
using AccountManagementServer.Application.Service;
using AccountManagementServer.Core.Interfaces;
using AccountManagementServer.Core.Services;
using AccountManagementServer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AccountManagementServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            string connectionString = "Server=KarenM-NB; Database=AccountManagementDb; Trusted_Connection=True;TrustServerCertificate=True;";
            builder.Services.AddDbContext<AccountMaagmentDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddSingleton<IAuthService, JwtAuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "AccountManagementServer",
                    ValidAudience = "AccountManagementReactApp",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b0deb9b3-2770-49f1-9c7f-accdce966e76"))
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeAdmin", policy => policy.RequireClaim("isAdmin", "True"));
            });

            builder.Services.AddCors(
            options => options.AddPolicy("myCorsPolicy",
            policy => policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod())
            );


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("myCorsPolicy");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
