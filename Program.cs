using Lesson2;
using Lesson2.Database;
using Lesson2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IMessageProvider>(provider =>
    new JsonMessageProvider(builder.Configuration["FilePath"] ?? "messages.json"));

builder.Services.AddScoped<IMessageService, MessageService>();

var app = builder.Build();

app.UseMiddleware<MaintenanceMiddleware>();
app.MapControllers();

app.Run();