using HotelSystem.Domain.Data;
using HotelSystem.Domain.Models;

namespace HotelSystem.Server.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с номерами гостиницы
/// </summary>
public class RoomService
{
    private readonly List<Room> _rooms;

    /// <summary>
    /// Загружает тестовые данные номеров
    /// </summary>
    public RoomService()
    {
        _rooms = HotelTestData.Rooms;
    }

    /// <summary>
    /// Возвращает список всех номеров
    /// </summary>
    public List<Room> GetAll()
    {
        return _rooms;
    }

    /// <summary>
    /// Возвращает номер по его идентификатору
    /// </summary>
    public Room? GetById(int id)
    {
        return _rooms.FirstOrDefault(room => room.Id == id);
    }

    /// <summary>
    /// Добавляет новый номер в список
    /// </summary>
    public Room Add(Room room)
    {
        room.Id = _rooms.Count == 0
            ? 1
            : _rooms.Max(existingRoom => existingRoom.Id) + 1;

        _rooms.Add(room);

        return room;
    }

    /// <summary>
    /// Обновляет данные существующего номера
    /// </summary>
    public bool Update(int id, Room room)
    {
        var existingRoom = GetById(id);

        if (existingRoom is null)
        {
            return false;
        }

        room.Id = id;

        var index = _rooms.IndexOf(existingRoom);
        _rooms[index] = room;

        return true;
    }

    /// <summary>
    /// Удаляет номер по его идентификатору
    /// </summary>
    public bool Delete(int id)
    {
        var room = GetById(id);

        if (room is null)
        {
            return false;
        }

        _rooms.Remove(room);

        return true;
    }
}