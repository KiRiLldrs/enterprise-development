using HotelSystem.Application.Repositories;
using HotelSystem.Application.Services;
using HotelSystem.Infrastructure.Repositories;
using HotelSystem.Infrastructure.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<HotelInMemoryStorage>();
builder.Services.AddSingleton<IClientRepository, InMemoryClientRepository>();
builder.Services.AddSingleton<IRoomTypeRepository, InMemoryRoomTypeRepository>();
builder.Services.AddSingleton<IRoomRepository, InMemoryRoomRepository>();
builder.Services.AddSingleton<IBookingRepository, InMemoryBookingRepository>();

builder.Services.AddSingleton<ClientService>();
builder.Services.AddSingleton<RoomTypeService>();
builder.Services.AddSingleton<RoomService>();
builder.Services.AddSingleton<BookingService>();
builder.Services.AddSingleton<AnalyticsService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.MapControllers();

app.Run();