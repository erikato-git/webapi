using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(options =>
{
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    string connStr;

    // for 'dotnet run'
    if (env == "Development")
    {
        connStr = builder.Configuration["ConnectionStrings:DevelopmentConnection"];
        options.UseNpgsql(connStr);
    }
    // for 'docker-compose'
    else
    {
        options.UseNpgsql(builder.Configuration["ConnectionStrings:ProductionConnection"]);
    }
});

builder.Services.AddScoped<IWeatherForecast, WeatherForecastRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
        .WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});


var app = builder.Build();

// Ensure postgres builds the db when running the docker-container
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Add 10 seconds delay to ensure the db server is up to accept connections
        // This won't be needed in a real-world application.
        System.Threading.Thread.Sleep(10000);
        var context = services.GetRequiredService<DataContext>();
        var created = context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

// --- Security ---

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // --- Security ---
    app.Use(async (context, next) => {
        context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");  // max-age=31536000 = 1 year
        context.Response.Headers.Add("X-Xss-Protection", "1");
        context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("Referrer-Policy", "no-referrer");
        context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self'; font-src 'self'; img-src 'self'; frame-src 'self'");
        

        await next();
    });
}

app.UseHttpsRedirection();   // more complicated with docker


app.UseAuthorization();
app.UseCors("CorsPolicy");

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");


app.Run();
