using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TransportationAPI.Data;
using TransportationAPI.Helper;
using TransportationAPI.Repository;
using TransportationAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swasxehbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options
    .UseSqlServer(builder
        .Configuration
        .GetConnectionString("Default")));

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();






builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        //options.Password.RequireDigit = false;
        //options.Password.RequiredLength = 4;
        //options.Password.RequireNonAlphanumeric = false;
        //options.Password.RequireUppercase = false;
        //options.Password.RequireLowercase = false;
    }).AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddSwaggerGen(options =>
{

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header

            },
            new List<string>()
        }

    });
});


builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(z =>
{
    z.RequireHttpsMetadata = false;
    z.SaveToken = true;
    z.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("APISetting:secret"))),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}


app.UseCors(policyName: "CorsPolicy");
app.Map("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();




app.MapControllers();

app.Run();