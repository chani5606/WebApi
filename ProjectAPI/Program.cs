using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectAPI.Data;
using ProjectAPI.Interfaces;
using ProjectAPI.Mapping;
using ProjectAPI.Repository;
using ProjectAPI.Services;
using ProjectFinal.Interfaces;
using Serilog;
using StoreApi.Services;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting Lottery API application");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "ProjectAPI", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "הכניסי: Bearer {token}"
        });

        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
        });
    });
    builder.Services.AddDbContext<LotteryContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
            ));

    builder.Services.AddScoped<IGiftsServices, GiftsService>();
    builder.Services.AddScoped<IDonorServicecs, DonorServices>();
    builder.Services.AddScoped<IUserServices, UserServices>();
    builder.Services.AddScoped<ICatgorySerices, CatgorySerices>();
    builder.Services.AddScoped<IBasketServices, BasketServices>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IWinnerServises, WinnerServises>();
    builder.Services.AddScoped<IReportServices, ReportServices>();
    builder.Services.AddScoped<ISaleServices, SaleServices>();




    builder.Services.AddScoped<IGiftRepository, GiftRepository>();
    builder.Services.AddScoped<IDonorRepository, DonorRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ICatgoryRepository, CatgoryRepository>();
    builder.Services.AddScoped<IBasketReapository, BasketReapository>();
    builder.Services.AddScoped<IWinnerRepository, WinnerRepository>();
    builder.Services.AddScoped<IReportRepository, ReportRepository>();
    builder.Services.AddScoped<ISaleReapository, SaleReapository>();


    //builder.Services.AddAutoMapper(typeof(GiftProfile));
    //builder.Services.AddAutoMapper(typeof(DonorProfile));
    //builder.Services.AddAutoMapper(typeof(UserProfile));
    //builder.Services.AddAutoMapper(typeof(CatgortProfile));
    //builder.Services.AddAutoMapper(typeof(BasketProfile));
    //builder.Services.AddAutoMapper(typeof(WinnerProfile));
    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.ShouldMapMethod = m => false;
    }, AppDomain.CurrentDomain.GetAssemblies());




    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey is not configured");

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // ⭐ חובה בלוקאל
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            RoleClaimType = System.Security.Claims.ClaimTypes.Role,   // ⭐ כאן
            NameClaimType = System.Security.Claims.ClaimTypes.NameIdentifier,
            ClockSkew = TimeSpan.Zero

        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>

            {
               

                Log.Warning("JWT Authentication failed: {Error}", context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var userId = context.Principal?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                Log.Debug("JWT token validated for user {UserId}", userId);
                return Task.CompletedTask;
            }
        };
    });

    builder.Services.AddAuthorization();

    // Configure JSON options to handle circular references
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("dev", policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    app.UseSerilogRequestLogging();

  

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors("dev");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
