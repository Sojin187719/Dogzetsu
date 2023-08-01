using Dogzetsu.Engine.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCustomSwagger();
builder.Services.AddControllers();
builder.Services.AddCustomVersioning();
 
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCustomSwagger();
app.Run();