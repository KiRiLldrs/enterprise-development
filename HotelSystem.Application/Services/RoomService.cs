using HotelSystem.Application.Repositories;
using HotelSystem.Domain.Models;

namespace HotelSystem.Application.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с комнатами
/// </summary>
public class RoomService(IRoomRepository roomRepository)
    : ICrudService<Room>
{
    /// <summary>
    /// Возвращает все комнаты
    /// </summary>
    public List<Room> GetAll() =>
        roomRepository.GetAll();

    /// <summary>
    /// Возвращает комнату по идентификатору
    /// </summary>
    public Room? GetById(int id) =>
        roomRepository.GetById(id);

    /// <summary>
    /// Добавляет новую комнату
    /// </summary>
    public Room Add(Room room) =>
        roomRepository.Add(room);

    /// <summary>
    /// Обновляет комнату
    /// </summary>
    public bool Update(int id, Room room) =>
        roomRepository.Update(id, room);

    /// <summary>
    /// Удаляет комнату
    /// </summary>
    public bool Delete(int id) =>
        roomRepository.Delete(id);
}