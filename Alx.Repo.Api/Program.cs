using Alx.Repo.Api.SwaggerExamples;
using Alx.Repo.Application;
using Alx.Repo.Application.Auth;
using Alx.Repo.Application.Helper;
using Alx.Repo.Application.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

//builder.Services.AddScoped<UserManager<ApplicationUser>>();


// Add standard DBContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
// Add commands and queries here
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Alx.Repo.Application.Query.GetItemQuery).Assembly));

// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddDefaultTokenProviders();


var jwtKey = configuration.GetSection("JWT:Secret").Value ?? throw new InvalidOperationException("Config[JWT:Secret] not found.");
// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, // Ensure expiration is validated
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero // Set clock skew to zero for immediate expiration validation
    };
});

builder.Services.AddCors();
//builder.Services.AddControllers();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
 {
     options.InvalidModelStateResponseFactory = context =>
     {
         var errors = context.ModelState
             .Where(m => m.Value?.Errors != null && m.Value.Errors.Any())
             .ToDictionary(
                 // m => m.Key == "$" ? "Error" : m.Key,
                 m => m.Key,
                 m => m.Value?.Errors?.Select(e => Utilities.FormatModelStateValidationError(e.ErrorMessage)).ToArray() ?? Array.Empty<string>()
             );

         var customResponse = new ErrorResponse
         {
             Message = "One or more validation errors occurred.",
             Errors = errors
         };

         return new BadRequestObjectResult(customResponse);
     };
 });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

Utilities.Initialize(configuration);
//builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerGen(c =>
{
    c.ExampleFilters();

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Alx.Repo", Version = "V1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        //In = ParameterLocation.Header,
        //Description = "Please paste 'Bearer {token}' to authenticate.",
        //Name = "Authorization",
        //Type = SecuritySchemeType.ApiKey,
        //Scheme = "Bearer"

        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<LoginUserExample>();
//builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

var app = builder.Build();

//Add default user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedApplicationUser.Initialize(services, configuration); // Call your seed method here
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
