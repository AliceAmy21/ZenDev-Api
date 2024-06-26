using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Net.Http.Headers;
using ZenDev.Api;
using ZenDev.BusinessLogic;
using ZenDev.Persistence;

var builder = WebApplication.CreateBuilder(args);

var CORS_POLICY_NAME = "CorsPolicy";
///////////////////////////////////////////////////////
// Services
///////////////////////////////////////////////////////
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("ZenDev");
builder.Services
    .AddDbContext<ZenDevDbContext>(options => options
        .UseSqlServer(connectionString)
    );

var allowedOriginsString = builder.Configuration.GetValue<string>("AllowedOrigins");
var allowedOrigins = !string.IsNullOrEmpty(allowedOriginsString) ? allowedOriginsString.Split([' ', ',', ';']) : [];
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY_NAME,
                      policy =>
                      {
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                          policy.WithOrigins(allowedOrigins);
                      });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApiDependencies();
builder.Services.RegisterBusinessLayerDependencies();

builder.Services
    .AddAuthentication()
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("EntraConfiguration"));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddHttpClient("strava", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://www.strava.com/api/v3/");
});

///////////////////////////////////////////////////////
// App configuration
///////////////////////////////////////////////////////
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DisplayRequestDuration();
    });
}

if (app.Configuration.GetValue<bool>("EnableSSL"))
{
    app.UseHttpsRedirection();
}


app.UseCors(CORS_POLICY_NAME);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
