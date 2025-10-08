using Microsoft.EntityFrameworkCore;
using NLog.Web;
using QueueSystem.Implement.ApplicationDbContext;
using QueueSystem.Implement.Repositories;
using QueueSystem.Implement.Repositories.Interface;
using QueueSystem.Implement.Services.Interface;
using QueueSystem.Implement.UnitOfWork;
using QueueSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowSpa", p => p
        .WithOrigins("http://localhost:5173",
        "http://10.21.10.1:8087")
        .AllowAnyHeader()
        .AllowAnyMethod());
});
// Db (SQL Server)
builder.Services.AddDbContext<QueueSystemDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICounterSequenceRepository, CounterSequenceRepository>();
builder.Services.AddTransient<ICountersRepository, CountersRepository>();
builder.Services.AddTransient<IPatronRepository, PatronRepository>();
builder.Services.AddTransient<IQueueTicketRepository, QueueTicketRepository>();
builder.Services.AddTransient<ITicketArchiveRepository, TicketArchiveRepository>();
builder.Services.AddTransient<IQueueService, QueueService>();


builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply migrations / create DB at startup
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        try
        {
            var db = scope.ServiceProvider.GetRequiredService<QueueSystemDbContext>();
            db.Database.Migrate();
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Database migration failed at startup");
            // Optionally rethrow in non-prod:
            // throw;
        }
    }
}
app.UseCors("AllowSpa");

app.UseHttpsRedirection();
app.UseMiddleware<ApiMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
