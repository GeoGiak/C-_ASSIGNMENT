
using Microsoft.EntityFrameworkCore;

using System.Collections.Concurrent;

using IPStackDLL;
using IPStackDLL.Interfaces;

using IPStackApi.Context;
using IPStackApi.Repositories;
using IPStackApi.Services;
using IPStackApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dynamic Link Library
builder.Services.AddSingleton<IIPInfoProvider>(provider =>
    new IPInfoProvider("d7478bcc6196b10ceb540030ceab650e")
);

// DbContext configuration with connection string
builder.Services.AddDbContext<AppDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Cache
builder.Services.AddSingleton<IPCacheService>();

// Singleton Dictionary for batch update jobs
builder.Services.AddSingleton<ConcurrentDictionary<Guid, BatchUpdateJob>>();

// All Services & Repositories
builder.Services.AddScoped<IPDetailsRepository>();
builder.Services.AddScoped<UpdateBatchJobService>();
builder.Services.AddScoped<IPDetailsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
