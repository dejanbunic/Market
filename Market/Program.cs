using Market.Domain;
using Market.Infrastructure;
using Market.Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Market
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            byte[] _key = Encoding.ASCII.GetBytes(Constants.JWT_PASSWORD);
        // Add services to the container.
        builder.Services
                .AddDbContext<MarketContext>();

            builder.Services.AddInfrastructure().AddPresentation();
            builder.Services.AddControllers();

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                x.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        return Task.CompletedTask;
                    }
                };
            });

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

            app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}