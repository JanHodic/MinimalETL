using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalETL.Server.Contracts.Modules;
using MinimalETL.Server.Data.Contexts;
using MinimalETL.Server.Data.Repositories;
using MinimalETL.Server.Data.Triggers;
using MinimalETL.Server.Models.Bases;
using MinimalETL.Server.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MinimalETL.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ILogger, Logger<BaseDateEntity>>();
var connectionString = builder.Configuration.GetConnectionString("MinimalETL");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException($"{nameof(connectionString)} not configured.");
builder.Services.AddDbContext<TestETLDbContext>(options =>
    options.UseSqlServer(connectionString).UseTriggers(triggerOptions =>
                triggerOptions
                    .AddTrigger<BaseUserDateTrigger>().MaxCascadeCycles(2)
        )
        .UseLazyLoadingProxies()
        .ConfigureWarnings(
            x => x.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning)
        ));
//registering of repositories
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddAutoMapper(typeof(AutomapperConfigurationProfile));
builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
