using Microsoft.EntityFrameworkCore;
using ProjectManager.Data.Context;
using ProjectManager.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDataLayer(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddControllers();



var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
