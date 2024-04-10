using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using wshop3.Datab;
using wshop3.Model;
using wshop3.Service;
using wshop3.Service.IAuth;
using static wshop3.Service.AuthService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<Wshop3Context>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("defaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("defaultConnection")));
});

//entity -hez
builder.Services.AddDbContext<IdentityContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("EntityConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("EntityConnection")));
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("AuthSettings:JwtOptions"));

builder.Services.AddIdentity<IdentityFelhasznalo, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthService, AuthService1>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();


//később cserélni jwt -re.
// builder.Services.AddAuthentication()
// .AddBearerToken(IdentityConstants.BearerScheme);

//??
// builder.Services.AddAuthorizationBuilder();

//?? -> jav
// builder.Services.AddIdentityCore<IdentityFelhasznalo>()
// .AddEntityFrameworkStores<IdentityContext>()
// .AddApiEndpoints();

var app = builder.Build();

// app.MapIdentityApi<IdentityFelhasznalo>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
