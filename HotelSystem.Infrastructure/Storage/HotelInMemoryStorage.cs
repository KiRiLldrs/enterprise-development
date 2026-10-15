using HotelSystem.Domain.Data;
using HotelSystem.Domain.Models;

namespace HotelSystem.Infrastructure.Storage;

/// <summary>
/// Единое хранилище данных гостиницы в памяти
/// </summary>
public class HotelInMemoryStorage
{
    public List<Client> Clients { get; } = HotelTestData.Clients;

    public List<RoomType> RoomTypes { get; } = HotelTestData.RoomTypes;

    public List<Room> Rooms { get; } = HotelTestData.Rooms;

    public List<Booking> Bookings { get; } = HotelTestData.Bookings;
}