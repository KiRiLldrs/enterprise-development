
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий номеров гостиницы с хранением данных в базе PostgreSQL
/// </summary>
public class EfRoomRepository(HotelDbContext context) : IRepository<Room>
{
    /// <inheritdoc />
    public List<Room> GetAll() =>
        context.Rooms
            .Include(room => room.RoomType)
            .ToList();

    /// <inheritdoc />
    public Room? GetById(int id) =>
        context.Rooms
            .Include(room => room.RoomType)
            .FirstOrDefault(room => room.Id == id);

    /// <inheritdoc />
    public Room Add(Room room)
    {
        context.Rooms.Add(room);
        context.SaveChanges();

        return room;
    }

    /// <inheritdoc />
    public bool Update(int id, Room room)
    {
        var existingRoom = GetById(id);

        if (existingRoom is null)
        {
            return false;
        }

        existingRoom.RoomNumber = room.RoomNumber;
        existingRoom.Floor = room.Floor;
        existingRoom.HasBalcony = room.HasBalcony;
        existingRoom.RoomType = room.RoomType;

        context.SaveChanges();

        return true;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var room = GetById(id);

        if (room is null)
        {
            return false;
        }

        context.Rooms.Remove(room);
        context.SaveChanges();

        return true;
    }
}
