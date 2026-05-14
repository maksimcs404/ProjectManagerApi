using Microsoft.EntityFrameworkCore;
using ProjectManager.Data.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EfContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();



var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
