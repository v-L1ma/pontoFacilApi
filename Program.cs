using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using pontoFacilApi.source.Application.Usecases.CadastrarUsuario;
using pontoFacilApi.source.Application.Usecases.LoginUsuario;
using pontoFacilApi.source.Domain.Models;
using pontoFacilApi.source.Infraestructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ponto fácil api",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Coloque o seu Token JWT aqui, Bearer[espaço] token"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference{
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers();

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});

builder.Services.AddDbContext<UsuarioDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});

builder.Services
        .AddIdentity<Usuario, IdentityRole>()
        .AddEntityFrameworkStores<UsuarioDbContext>()
        .AddDefaultTokenProviders();

builder.Services.AddScoped<ICadastrarUsuarioUseCase, CadastrarUsuarioUseCase>();
builder.Services.AddScoped<ILoginUsuarioUsecase, LoginUsuarioUsecase>();
builder.Services.AddScoped<IEditarPermissoesUsuarioUsecase, EditarPermissoesUsuarioUsecase>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddSingleton<IAuthorizationHandler, AdministradorAuthorization>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CargoAdministrador", policy => policy.AddRequirements(new CargoAdministrador("Administrador")));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"]!)),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeeder.SeedRolesAndAdminAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();