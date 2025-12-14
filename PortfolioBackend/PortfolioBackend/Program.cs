using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Portfolio_Backend_API"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(policy =>
                      {
                          //policy.WithOrigins("https://kylehennessy.azurewebsites.net", "http://localhost:3000")
                          policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      }
                    );
});


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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWT:Secret") ?? string.Empty)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5),
        RequireSignedTokens = true
    };
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
