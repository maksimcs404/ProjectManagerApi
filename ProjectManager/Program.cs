using Microsoft.EntityFrameworkCore;
using ProjectManager.Data.Context;
using ProjectManager.Data;
using ProjectManager.Application.Interfaces;
using ProjectManager.Application.Services;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDataLayer(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();



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
