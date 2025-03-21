using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using QuizAPI.API.Middlewares;
using QuizAPI.Application;
using QuizAPI.Application.Validators;
using QuizAPI.Infrastructure;
using QuizAPI.Infrastructure.Filters;
using QuizAPI.Persistence;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.Elasticsearch;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Services.AddControllers();

//ValidatorFilter kullanýlmadý.
//builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
//    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandRequestValidator>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });

builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy => policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
    .AllowAnyMethod().AllowAnyHeader()));


var elasticsearchUri = builder.Configuration["Elasticsearch:Uri"] ?? "http://localhost:9200";

Log.Logger = new LoggerConfiguration()
     .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticsearchUri))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "logs-{0:yyyy.MM.dd}", 
    })
    .CreateLogger();

builder.Host.UseSerilog(); 

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "RedisInstance";
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>(); //tam olarak burada olmalý.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var imagesPath = Path.Combine(builder.Environment.ContentRootPath, "Images");

if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/api/images"
});

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
