using HotelSystem.Domain.Models;
using HotelSystem.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для выполнения CRUD-операций с номерами гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly RoomService _roomService;

    /// <summary>
    /// Инициализирует контроллер номеров
    /// </summary>
    public RoomsController(RoomService roomService)
    {
        _roomService = roomService;
    }

    /// <summary>
    /// Возвращает список всех номеров гостиницы
    /// </summary>
    [HttpGet]
    public IActionResult GetRooms()
    {
        return Ok(_roomService.GetAll());
    }

    /// <summary>
    /// Возвращает номер по его идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public IActionResult GetRoom(int id)
    {
        var room = _roomService.GetById(id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    /// <summary>
    /// Создаёт новый номер гостиницы
    /// </summary>
    [HttpPost]
    public IActionResult CreateRoom(Room room)
    {
        var createdRoom = _roomService.Add(room);

        return CreatedAtAction(
            nameof(GetRoom),
            new { id = createdRoom.Id },
            createdRoom);
    }

    /// <summary>
    /// Обновляет данные существующего номера
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult UpdateRoom(int id, Room room)
    {
        var updated = _roomService.Update(id, room);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет номер по его идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteRoom(int id)
    {
        var deleted = _roomService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}