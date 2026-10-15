using HotelSystem.Application.Repositories;
using HotelSystem.Domain.Models;

namespace HotelSystem.Application.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с типами номеров
/// </summary>
public class RoomTypeService(IRoomTypeRepository roomTypeRepository)
{
    /// <summary>
    /// Возвращает все типы номеров
    /// </summary>
    public List<RoomType> GetAll() =>
        roomTypeRepository.GetAll();

    /// <summary>
    /// Возвращает тип номера по идентификатору
    /// </summary>
    public RoomType? GetById(int id) =>
        roomTypeRepository.GetById(id);

    /// <summary>
    /// Добавляет новый тип номера
    /// </summary>
    public RoomType Add(RoomType roomType) =>
        roomTypeRepository.Add(roomType);

    /// <summary>
    /// Обновляет тип номера
    /// </summary>
    public bool Update(int id, RoomType roomType) =>
        roomTypeRepository.Update(id, roomType);

    /// <summary>
    /// Удаляет тип номера
    /// </summary>
    public bool Delete(int id) =>
        roomTypeRepository.Delete(id);
}