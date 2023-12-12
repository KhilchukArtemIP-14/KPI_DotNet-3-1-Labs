using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using GoodsStorage.DAL.Data;
using GoodsStorage.DAL.Repositories;
using GoodsStorage.BAL.Services;
using GoodsStorage.DAL.Repositories.Interfaces;
using GoodsStorage.DAL.Repositories.Implementations;
using GoodsStorage.BAL.Services.Interfaces;
using GoodsStorage.BAL.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using GoodsStorage.API.Authorization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GoodsStorageDbContext>(options => {
    options.UseSqlServer(connection, b => b.MigrationsAssembly("GoodsStorage.API"));
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Goods Storage Api", Version = "v1" });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddScoped<IGoodRepository,GoodRepository>();
builder.Services.AddScoped<IGoodsService, GoodsService>();

builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<IRequestService, RequestService>();

builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUserSummaryService, UserSummaryService>();

builder.Services.AddScoped<IAuthorizationHandler, CanAccessPurchaseHandler>();
builder.Services.AddScoped<IAuthorizationHandler, CanAccessRequestHandler>();
builder.Services.AddScoped<IAuthorizationHandler, CanAccessUserSummaryHandler>();

builder.Services.AddScoped<IAuthorizationRequirement, CanAccessPurchaseRequirement>();
builder.Services.AddScoped<IAuthorizationRequirement, CanAccessRequestRequirement>();
builder.Services.AddScoped<IAuthorizationRequirement, CanAccessUserSummaryRequirement>();

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("GoodsStorage")
    .AddEntityFrameworkStores<GoodsStorageDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanAccessUserSummaryPolicy", policy =>
    {
        policy.Requirements.Add(new CanAccessUserSummaryRequirement());
    });

    options.AddPolicy("CanAccessRequestPolicy", policy =>
    {
        policy.Requirements.Add(new CanAccessRequestRequirement());
    });

    options.AddPolicy("CanAccessPurchasePolicy", policy =>
    {
        policy.Requirements.Add(new CanAccessPurchaseRequirement());
    });
});

var app = builder.Build();

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