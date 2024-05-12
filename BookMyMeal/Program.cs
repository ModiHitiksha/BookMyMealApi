using BookMyMeal.MealDbContext;
using BookMyMeal.Repository.Implementation;
using BookMyMeal.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<MealDbContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<ICalenderRepository,CalenderRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IPostCouponRepository, PostCouponRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    /*  app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","DemoJWTToken v1"));*/
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(s =>
s.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
   );
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
