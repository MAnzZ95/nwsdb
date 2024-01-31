using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Nwsdb.Web.Api.Brokers.DateTimes;
using Nwsdb.Web.Api.Brokers.Loggings;
using Nwsdb.Web.Api.Brokers.Storages;
using Nwsdb.Web.Api.Services.Foundations.Districts;
using Nwsdb.Web.Api.Services.Foundations.DSDivisions;
using Nwsdb.Web.Api.Services.Foundations.GSDivisions;
using Nwsdb.Web.Api.Services.Foundations.Lands;
using Nwsdb.Web.Api.Services.Foundations.LandSubCategories;
using Nwsdb.Web.Api.Services.Foundations.LandTypes;
using Nwsdb.Web.Api.Services.Foundations.OwnerShips;
using Nwsdb.Web.Api.Services.Foundations.Provinces;
using Nwsdb.Web.Api.Services.Foundations.RMOs;
using Nwsdb.Web.Api.Services.Foundations.RSCs;
using Nwsdb.Web.Api.Services.Foundations.Users;
using Nwsdb.Web.Api.Services.Foundations.UserTypes;
using Nwsdb.Web.Api.Services.Foundations.Wsses;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<StorageBroker>();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = configuration["Keycloak:Authority"];
    options.Audience = configuration["Keycloak:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = configuration["Keycloak:Authority"],
        ValidAudience = configuration["Keycloak:Audience"]
    };
});

builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
builder.Services.AddTransient<IDateTimeBroker, DateTimeBroker>();
builder.Services.AddTransient<IUserTypeService, UserTypeService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ILandService, LandService>();
builder.Services.AddTransient<IDistrictService, DistrictService>();
builder.Services.AddTransient<IRscService, RscService>();
builder.Services.AddTransient<IRmoService, RmoService>();
builder.Services.AddTransient<IWssService, WssService>();
builder.Services.AddTransient<IProvinceService, ProvinceService>();
builder.Services.AddTransient<IDSDivisionService, DSDivisionService>();
builder.Services.AddTransient<IGSDivisionService, GSDivisionService>();
builder.Services.AddTransient<IOwnerShipService, OwnerShipService>();
builder.Services.AddTransient<IUserTypeService,UserTypeService>();
builder.Services.AddTransient<ILandTypeService, LandTypeService>();
builder.Services.AddTransient<ILandSubCategoryService, LandSubCategoryService>();

builder.Services.AddControllers().AddOData(opt =>
    opt.Select()
        .Filter()
        .OrderBy()
        .SetMaxTop(null)
        .Count());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Replace with your Angular app's URL
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntit

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

app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
