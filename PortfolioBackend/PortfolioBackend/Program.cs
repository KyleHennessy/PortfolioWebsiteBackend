using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PortfolioBackend.Models;
using PortfolioBackend.Models.Interfaces;
using PortfolioBackend.Services;
using PortfolioBackend.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.Configure<PortfolioDatabaseSettings>(
                builder.Configuration.GetSection(nameof(PortfolioDatabaseSettings)));

builder.Services.AddSingleton<IPortfolioDatabaseSettings>(sp =>
        sp.GetRequiredService<IOptions<PortfolioDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("PortfolioDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<ISkillRepository, SkillRepository>();

builder.Services.AddScoped<IWorkExperienceRepository, WorkExperienceRepository>();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWT:Secret").ToString())),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                                                        //.WithOrigins("https://localhost:3000")); // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials()); // allow credentials

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
