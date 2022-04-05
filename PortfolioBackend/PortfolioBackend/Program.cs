using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PortfolioBackend.Models;
using PortfolioBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<PortfolioDatabaseSettings>(
                builder.Configuration.GetSection(nameof(PortfolioDatabaseSettings)));

builder.Services.AddSingleton<IPortfolioDatabaseSettings>(sp =>
        sp.GetRequiredService<IOptions<PortfolioDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("PortfolioDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
