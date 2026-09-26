using HotelSystem.Domain.Models;
using HotelSystem.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для выполнения CRUD-операций с типами номеров гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomTypesController : ControllerBase
{
    private readonly RoomTypeService _roomTypeService;

    /// <summary>
    /// Инициализирует контроллер типов номеров
    /// </summary>
    public RoomTypesController(RoomTypeService roomTypeService)
    {
        _roomTypeService = roomTypeService;
    }

    /// <summary>
    /// Возвращает список всех типов номеров
    /// </summary>
    [HttpGet]
    public IActionResult GetRoomTypes()
    {
        return Ok(_roomTypeService.GetAll());
    }

    /// <summary>
    /// Возвращает тип номера по его идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public IActionResult GetRoomType(int id)
    {
        var roomType = _roomTypeService.GetById(id);

        if (roomType is null)
        {
            return NotFound();
        }

        return Ok(roomType);
    }

    /// <summary>
    /// Создаёт новый тип номера
    /// </summary>
    [HttpPost]
    public IActionResult CreateRoomType(RoomType roomType)
    {
        var createdRoomType = _roomTypeService.Add(roomType);

        return CreatedAtAction(
            nameof(GetRoomType),
            new { id = createdRoomType.Id },
            createdRoomType);
    }

    /// <summary>
    /// Обновляет данные существующего типа номера
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult UpdateRoomType(int id, RoomType roomType)
    {
        var updated = _roomTypeService.Update(id, roomType);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет тип номера по его идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteRoomType(int id)
    {
        var deleted = _roomTypeService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}