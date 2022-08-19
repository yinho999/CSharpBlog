using System.Text;
using CSharpBlog.Exception.Middleware;
using CSharpBlog.Repository.Context;
using CSharpBlog.Repository.Models;
using CSharpBlog.Service.Configuration;
using CSharpBlog.Service.Contracts;
using CSharpBlog.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        // generate the swagger documentation from xml files 
        var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
        foreach (var xmlFile in xmlFiles)
        {
            options.IncludeXmlComments(xmlFile);

        }
    }
);

// Add cors to allow cross-origin requests
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BloggingContext>(options => 
    options.UseNpgsql(connectionString, b=>b.MigrationsAssembly("CSharpBlog")));

builder.Services.AddIdentity<User,UserRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddTokenProvider<DataProtectorTokenProvider<User>>(builder.Configuration["Authentication:JwtBearer:LoginProvider"])
    .AddEntityFrameworkStores<BloggingContext>()
    .AddDefaultTokenProviders();


// Add serilog to the host 
builder.Host.UseSerilog( (ctx , lc) =>  lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MapperConfig));

// Add Repositories Dependency Injection
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<IBlogsRepository, BlogsRepository>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

// Inject Jwt Authentication
builder.Services
    .AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Authentication:JwtBearer:Issuer"],
            ValidAudience = builder.Configuration["Authentication:JwtBearer:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JwtBearer:SigningCredentials:SigningKey"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use exception handler middleware to log unhandled exceptions
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();