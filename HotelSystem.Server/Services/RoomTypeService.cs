using HotelSystem.Domain.Data;
using HotelSystem.Domain.Models;

namespace HotelSystem.Server.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с типами номеров гостиницы
/// </summary>
public class RoomTypeService
{
    private readonly List<RoomType> _roomTypes;

    /// <summary>
    /// Загружает тестовые данные типов номеров
    /// </summary>
    public RoomTypeService()
    {
        _roomTypes = HotelTestData.RoomTypes;
    }

    /// <summary>
    /// Возвращает список всех типов номеров
    /// </summary>
    public List<RoomType> GetAll()
    {
        return _roomTypes;
    }

    /// <summary>
    /// Возвращает тип номера по его идентификатору
    /// </summary>
    public RoomType? GetById(int id)
    {
        return _roomTypes.FirstOrDefault(roomType => roomType.Id == id);
    }

    /// <summary>
    /// Добавляет новый тип номера в список
    /// </summary>
    public RoomType Add(RoomType roomType)
    {
        roomType.Id = _roomTypes.Count == 0
            ? 1
            : _roomTypes.Max(existingRoomType => existingRoomType.Id) + 1;

        _roomTypes.Add(roomType);

        return roomType;
    }

    /// <summary>
    /// Обновляет данные существующего типа номера
    /// </summary>
    public bool Update(int id, RoomType roomType)
    {
        var existingRoomType = GetById(id);

        if (existingRoomType is null)
        {
            return false;
        }

        roomType.Id = id;

        var index = _roomTypes.IndexOf(existingRoomType);
        _roomTypes[index] = roomType;

        return true;
    }

    /// <summary>
    /// Удаляет тип номера по его идентификатору
    /// </summary>
    public bool Delete(int id)
    {
        var roomType = GetById(id);

        if (roomType is null)
        {
            return false;
        }

        _roomTypes.Remove(roomType);

        return true;
    }
}