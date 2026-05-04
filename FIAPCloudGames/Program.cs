using Core.Repository;
using Core.Service;
using FIAPCloudGames.Configurations;
using Infrastructure.Correlation;
using Infrastructure.Logging;
using Infrastructure.Middleware;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddCorrelationIdAccessor();
builder.Services.AddScoped(typeof(BaseLogger<>));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IJogoService, JogoService>();
builder.Services.AddScoped<IBibliotecaJogoService, BibliotecaJogoService>();
builder.Services.AddScoped<IAuthService>(sp =>
{
    var config = builder.Configuration;

    return new AuthService(
        sp.GetRequiredService<IUsuarioRepository>(),
        config["Jwt:Key"],
        config["Jwt:Issuer"],
        config["Jwt:Audience"]
    );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<IBibliotecaJogoRepository, BibliotecaJogoRepository>();

#region [JWT]

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
});

#endregion

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Configura o banco SQL Server usando a connection string do appsettings
    options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));

    // Habilita Lazy Loading (carregamento automático de entidades relacionadas)
    options.UseLazyLoadingProxies();

}, ServiceLifetime.Scoped);

var app = builder.Build();
app.UseCorrelationMiddleware();
app.UseGlobalExceptionMiddleware();
app.UseRequestLoggingMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();