using HotelSystem.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<ClientService>();
builder.Services.AddSingleton<RoomTypeService>();
builder.Services.AddSingleton<RoomService>();
builder.Services.AddSingleton<BookingService>();

var app = builder.Build();

app.MapControllers();

app.Run();