using smarthealth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using smarthealth.Data;
using smarthealth.Services;
using smarthealth.Service;
using AutoMapper;
using smarthealth;
using smarthealth.Repo;
using smarthealth.Utility;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("corspolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "https://react-smarthealth.vercel.app")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.AddMetricServer(options =>
{
    options.Port = 5678;
});
//Populate the values in JWT Options
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
StaticDetail.GeminiAPIBase = builder.Configuration["ServiceUrls:GeminiAPI"];
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient<IGeminiAiService, GeminiAiService>();

builder.Services.AddTransient<IMedicineRepo, MedicineRepo>();
builder.Services.AddTransient<IDoctorsRepo, DoctorsRepo>();
builder.Services.AddTransient<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddTransient<IAppointmentRepo, AppointmentRepo>();
builder.Services.AddTransient<IMedicalRecordRepo, MedicalRecordRepo>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IGeminiAiService,GeminiAiService>();

// Learn more about configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("corspolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpMetrics();
app.MapMetrics();
app.MapControllers();
ApplyMigrations();
app.Run();

void ApplyMigrations()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Any())
        {
            _db.Database.Migrate();
        }
    }
}

