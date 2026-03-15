using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Services;
using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Validators;
using SIGEBI.Domain.Validators.Interfaces;
using SIGEBI.Infrastructure.Interfaces;
using SIGEBI.Infrastructure.Services;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories;
using System.Text;

namespace SIGEBI.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SigebiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SigebiDb")));

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IUsuarioDomainRepository, UsuarioRepository>();
            builder.Services.AddScoped<IRolRepository, RolRepository>();
            builder.Services.AddScoped<IRolDomainRepository, RolRepository>();
            builder.Services.AddScoped<IRecursoBibliograficoRepository, RecursoBibliograficoRepository>();
            builder.Services.AddScoped<IRecursoBibliograficoDomainRepository, RecursoBibliograficoRepository>();
            builder.Services.AddScoped<IEjemplarRepository, EjemplarRepository>();
            builder.Services.AddScoped<IEjemplarDomainRepository, EjemplarRepository>();
            builder.Services.AddScoped<IPrestamoRepository, PrestamoRepository>();
            builder.Services.AddScoped<IPrestamoDomainRepository, PrestamoRepository>();
            builder.Services.AddScoped<IDevolucionRepository, DevolucionRepository>();
            builder.Services.AddScoped<IDevolucionDomainRepository, DevolucionRepository>();
            builder.Services.AddScoped<IPenalizacionRepository, PenalizacionRepository>();
            builder.Services.AddScoped<IPenalizacionDomainRepository, PenalizacionRepository>();
            builder.Services.AddScoped<INotificacionRepository, NotificacionRepository>();
            builder.Services.AddScoped<INotificacionDomainRepository, NotificacionRepository>();
            builder.Services.AddScoped<IAuditoriaRepository, AuditoriaRepository>();
            builder.Services.AddScoped<IAuditoriaDomainRepository, AuditoriaRepository>();

            builder.Services.AddScoped<IUsuarioValidator, UsuarioValidator>();
            builder.Services.AddScoped<IRolValidator, RolValidator>();
            builder.Services.AddScoped<IRecursoBibliograficoValidator, RecursoBibliograficoValidator>();
            builder.Services.AddScoped<IEjemplarValidator, EjemplarValidator>();
            builder.Services.AddScoped<IPrestamoValidator, PrestamoValidator>();
            builder.Services.AddScoped<IDevolucionValidator, DevolucionValidator>();
            builder.Services.AddScoped<IPenalizacionValidator, PenalizacionValidator>();
            builder.Services.AddScoped<INotificacionValidator, NotificacionValidator>();
            builder.Services.AddScoped<IAuditoriaValidator, AuditoriaValidator>();

            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IRolService, RolService>();
            builder.Services.AddScoped<IRecursoBibliograficoService, RecursoBibliograficoService>();
            builder.Services.AddScoped<IEjemplarService, EjemplarService>();
            builder.Services.AddScoped<IPrestamoService, PrestamoService>();
            builder.Services.AddScoped<IDevolucionService, DevolucionService>();
            builder.Services.AddScoped<IPenalizacionService, PenalizacionService>();
            builder.Services.AddScoped<INotificacionService, NotificacionService>();
            builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();
            builder.Services.AddScoped<IReporteService, ReporteService>();
            builder.Services.AddScoped<IJwtService, JwtService>();

            string jwtKey = builder.Configuration["Jwt:Key"] ?? string.Empty;
            string jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? string.Empty;
            string jwtAudience = builder.Configuration["Jwt:Audience"] ?? string.Empty;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
