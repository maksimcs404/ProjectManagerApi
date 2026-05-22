using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManager.Application.Interfaces;
using ProjectManager.Application.Services;
using ProjectManager.Data;
using ProjectManager.Data.Context;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDataLayer(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddControllers().AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AuthOptions:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AuthOptions:Audience"],
            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthOptions:Key"]!))
        };
    });




var app = builder.Build();

app.Use(async (context, next) =>
{
    Stopwatch stopwatch = Stopwatch.StartNew();

    await next.Invoke();

    stopwatch.Stop();
    Console.WriteLine($"[{context.Request.Method}]: {context.Request.Path} from {context.Connection.RemoteIpAddress}. \n Elapsed: {stopwatch.ElapsedMilliseconds} ms.\n");
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
