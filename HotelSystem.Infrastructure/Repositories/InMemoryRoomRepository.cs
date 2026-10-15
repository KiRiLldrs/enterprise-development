using HotelSystem.Application.Repositories;
using HotelSystem.Domain.Models;
using HotelSystem.Infrastructure.Storage;

namespace HotelSystem.Infrastructure.Repositories;

/// <summary>
/// Репозиторий комнат гостиницы с хранением данных в памяти
/// </summary>
public class InMemoryRoomRepository(HotelInMemoryStorage storage)
    : IRoomRepository
{
    public List<Room> GetAll() => storage.Rooms;

    public Room? GetById(int id) =>
        storage.Rooms.FirstOrDefault(room => room.Id == id);

    public Room Add(Room room)
    {
        room.Id = storage.Rooms.Count == 0
            ? 1
            : storage.Rooms.Max(existing => existing.Id) + 1;

        storage.Rooms.Add(room);

        return room;
    }

    public bool Update(int id, Room room)
    {
        var index = storage.Rooms.FindIndex(existing => existing.Id == id);

        if (index == -1)
        {
            return false;
        }

        room.Id = id;
        storage.Rooms[index] = room;

        return true;
    }

    public bool Delete(int id)
    {
        var room = GetById(id);

        return room is not null && storage.Rooms.Remove(room);
    }
}