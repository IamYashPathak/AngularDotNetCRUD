using CRUD_OPS1.ExceptionHandlers;
using CRUD_OPS1.Model;
using CRUD_OPS1.Model.Data;
using CRUD_OPS1.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CRUD_OPS1
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors();

            builder.Services.AddTransient<DapperDBContext>();
            builder.Services.AddTransient<IEmployeeRepo,EmployeeRepo>();
            builder.Services.AddTransient<ICredentialRepo,CredentialRepo>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => { 
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters { 
                ValidateIssuer = true ,
                ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey= true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddExceptionHandler<AppExceptionHandler>();

            //options => {
            //    options.AddPolicy(IdentityData.AdminUserPolicyName, p =>
            //        p.RequireClaim(IdentityData.AdminUserClaimName, "1"));
            //}

            var app = builder.Build();

            app.UseExceptionHandler(_ => { });
            app.UseAuthentication();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
