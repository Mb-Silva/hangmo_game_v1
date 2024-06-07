using Hangmo.Server.Services.Interfaces;
using Hangmo.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Hangmo.Services.Interfaces;
using Hangmo.Services;
using Hangmo.Repository.Data.Entities;
using Hangmo.Repository.Data;
using Hangmo.Repository.Data.DAO.Interfaces;
using Hangmo.Repository.Data.DAO;
using Hangmo.Repository.Services;
using Hangmo.Server.Helpers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddHttpClient(); //Utilizado para a conexão com a IA

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionDb")));

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication();
builder.Services.AddHealthChecks();
builder.Services.AddCors();

builder.Services.AddScoped<IWordGenerationService, GeminiService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IWordService, WordService>();
builder.Services.AddScoped<IBaseDAO<Word>, WordDAO>();
builder.Services.AddScoped<IBaseDAO<Game>, GameDAO>();
builder.Services.AddScoped<BaseService<Word>, WordService>();
builder.Services.AddScoped<WordDAO>();

//builder.Services.AddHostedService<HostedWordGeneration>(); // Registra o HostedWordGeneration como um serviço hospedado


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configura o Swagger para aparecer em produção.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.MapIdentityApi<AppUser>();
app.UseHttpsRedirection();
// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()); // allow credentials

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
