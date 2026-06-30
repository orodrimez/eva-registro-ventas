using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegistroVentas.Api.Business;
using RegistroVentas.Api.Data;
using RegistroVentas.Api.Endpoints;
using RegistroVentas.Api.Infrastructure;
using RegistroVentas.Api.OpenApi;
using RegistroVentas.Api.Security;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.Configure<CryptoOptions>(builder.Configuration.GetSection("Crypto"));

builder.Services.AddDbContext<RegistroDbContext>(options =>
{
    options.UseSqlite( builder.Configuration.GetConnectionString("Sqlite"));
});

builder.Services.AddSingleton<Aes256CryptoService>();
builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddScoped<IRegistroOperacionService, RegistroOperacionService>();

var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("Jwt:Issuer no configurado.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException("Jwt:Audience no configurado.");

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key no configurado.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,

            ValidateAudience = true,
            ValidAudience = jwtAudience,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey) ),

            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RolValido", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context =>
            RoleHelper.HasAnyRole(context.User, "Operador", "Supervisor"));
    });

    options.AddPolicy("Operador", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context =>
            RoleHelper.HasAnyRole(context.User, "Operador"));
    });

    options.AddPolicy("Supervisor", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireAssertion(context =>
            RoleHelper.HasAnyRole(context.User, "Supervisor"));
    });
});

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<JwtOpenApiTransformer>();
    options.AddOperationTransformer<JwtOpenApiTransformer>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Registro Ventas API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RegistroDbContext>();
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<RegistroDbContext>();

    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("RegistroVentas.Api.Startup");

    await db.Database.OpenConnectionAsync();

    try
    {
        using var command = db.Database.GetDbConnection().CreateCommand();

        command.CommandText = "select sqlite_version();";

        var version = await command.ExecuteScalarAsync();

        logger.LogInformation("SQLite runtime version: {Version}", version);
    }
    finally
    {
        await db.Database.CloseConnectionAsync();
    }
}


app.UseAuthentication();

app.UseAuthorization();

app.MapAuthCryptoEndpoints();

app.MapRegistroEndpoints();

app.Run();


