using HotelSystem.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<ClientService>();

var app = builder.Build();

app.MapControllers();

app.Run();