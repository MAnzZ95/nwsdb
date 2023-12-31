using FluentAssertions.Common;
using Newtonsoft.Json;
using Nwsdb.Web.Api.Brokers.DateTimes;
using Nwsdb.Web.Api.Brokers.Loggings;
using Nwsdb.Web.Api.Brokers.Storages;
using Nwsdb.Web.Api.Services.Foundations.Districts;
using Nwsdb.Web.Api.Services.Foundations.Lands;
using Nwsdb.Web.Api.Services.Foundations.RMOs;
using Nwsdb.Web.Api.Services.Foundations.RSCs;
using Nwsdb.Web.Api.Services.Foundations.Users;
using Nwsdb.Web.Api.Services.Foundations.UserTypes;
using Nwsdb.Web.Api.Services.Foundations.Wsses;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<StorageBroker>();
builder.Services.AddHttpClient();

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
builder.Services.AddTransient<IUserService, UserService>();



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
