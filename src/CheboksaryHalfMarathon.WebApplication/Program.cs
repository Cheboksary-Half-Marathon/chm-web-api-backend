using CheboksaryHalfMarathon.DAL;
using CheboksaryHalfMarathon.WebAplication.Config;
using CheboksaryHalfMarathon.WebAplication.OData;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddSwaggerGen();


var env = builder.Environment;
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration = configurationBuilder.Build();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));

// Add services to the container.
var conn = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HalfMarathonDbContext>(options =>
    options.UseSqlServer(conn));

builder.Services.AddScoped<IConventionModelFactory, EdmModelFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Account/Login";
//    });

//builder.Services.AddAuthorization(options =>
//{
//    // Configure your policies here (see next section)
//});



builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
