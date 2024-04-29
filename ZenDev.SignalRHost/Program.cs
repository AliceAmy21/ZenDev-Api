using ZenDev.SignalRHost.Hubs;

var builder = WebApplication.CreateBuilder(args);

var CORS_POLICY_NAME = "CorsPolicy";
///////////////////////////////////////////////////////
// Services
///////////////////////////////////////////////////////

var allowedOriginsString = builder.Configuration.GetValue<string>("AllowedOrigins");
var allowedOrigins = !string.IsNullOrEmpty(allowedOriginsString) ? allowedOriginsString.Split([' ', ',', ';']) : [];
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY_NAME,
                      policy =>
                      {
                          policy.AllowCredentials();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                          policy.WithOrigins(allowedOrigins);
                      });
});

builder.Services.AddRazorPages();

builder.Services.AddSignalR(); // Required for SignalR

///////////////////////////////////////////////////////
// App configuration
///////////////////////////////////////////////////////
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(CORS_POLICY_NAME);

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<ExampleHub>(ExampleHub.HUB_IDENTIFIER); // Required for SignalR

app.Run();
