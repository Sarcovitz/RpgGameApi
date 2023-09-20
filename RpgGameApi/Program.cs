using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RpgGame.Configuration;
using RpgGame.Data;
using RpgGame.Middlewares;
using RpgGame.Providers;
using RpgGame.Providers.Interfaces;
using RpgGame.Repositories;
using RpgGame.Repositories.Interfaces;
using RpgGame.Services;
using RpgGame.Services.Interfaces;
using System.Text;

ILog _logger = LogManager.GetLogger(typeof(Program));

var builder = WebApplication.CreateBuilder(args);

XmlConfigurator.Configure(new FileInfo($"log4net.{builder.Environment.EnvironmentName}.config"));

//Configuration
builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true);

var emailConfig = builder.Configuration.GetSection("EmailConfig");
builder.Services.Configure<EmailConfig>(emailConfig);
var appConfig = builder.Configuration.GetSection("AppConfig");
builder.Services.Configure<AppConfig>(appConfig);

//BASE
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

//DB
builder.Services.AddDbContext<GameDbContext>(ServiceLifetime.Scoped, ServiceLifetime.Singleton);

//SERVICES
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<ICryptographyService, CryptographyService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IUserService, UserService>();

//REPOSITORIES
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//PROVIDERS
builder.Services.AddSingleton<ITimeHelper, TimeHelper>();

//MIDDLEWARE
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

//AUTH
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AppConfig:secret"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

_logger.Info("@@@@@ Program is staring @@@@@");

app.Run();
