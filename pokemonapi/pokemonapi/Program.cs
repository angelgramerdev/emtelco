using application.Interfaces;
using application.Services;
using common;
using domain.Entities;
using domain.Interfaces.Repositories;
using infraestructure.Contexts;
using infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;
using System.Text.Json.Serialization;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("log error api");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDistributedMemoryCache();

    builder.Services.AddSession(options =>
    {
        //options.IdleTimeout = TimeSpan.FromSeconds(10);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });
    builder.Services.AddMvc();

    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        //options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    });

    builder.Services.ConfigureApplicationCookie(options =>
    {
        // Cookie settings
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.SlidingExpiration = true;
    });
    builder.Services.AddControllers().AddJsonOptions(x =>
                   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    builder.Services.AddScoped<IPokemonService, PokemonService>();
    builder.Services.AddScoped<IObjResponse, ObjResponse>();
    builder.Services.AddScoped<IPokemonRepository<Pokemon>, PokemonRepository>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
    builder.Services.AddScoped<IAuthenticateRepository<ObjIdentity>, AuthenticateRepository>();
    builder.Services.AddScoped<IHabilityService, HabilityService>();
    builder.Services.AddScoped<IHabilityRepository<Hability>, HabilityRepository>();
    builder.Services.AddDbContext<PokemonContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("conexionSql")));
    builder.Services.AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<PokemonContext>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "devCorsPolicy",
                          policy =>
                          {
                              policy.WithOrigins("https://localhost", "http://localhost");
                          });
    });

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opt => {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    //app.UseAuthorization();
    app.UseRouting();
    app.UseCors("devCorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();

}
catch (Exception e)
{
    logger.Error(e,"Program has stop due to error");
    throw;
}
finally 
{
    NLog.LogManager.Shutdown();
}
