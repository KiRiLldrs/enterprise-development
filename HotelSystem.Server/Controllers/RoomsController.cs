using HotelSystem.Application.Contracts.Rooms;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для выполнения CRUD-операций с номерами гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomsController(
    RoomService roomService,
    RoomTypeService roomTypeService) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех номеров гостиницы
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<RoomResponse>), StatusCodes.Status200OK)]
    public ActionResult<List<RoomResponse>> GetRooms()
    {
        var rooms = roomService.GetAll()
            .Select(ToResponse)
            .ToList();

        return Ok(rooms);
    }

    /// <summary>
    /// Возвращает номер по его идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RoomResponse> GetRoom(int id)
    {
        var room = roomService.GetById(id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(ToResponse(room));
    }

    /// <summary>
    /// Создаёт новый номер гостиницы
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<RoomResponse> CreateRoom(CreateRoomRequest request)
    {
        var roomType = roomTypeService.GetById(request.RoomTypeId);

        if (roomType is null)
        {
            return BadRequest("Тип номера с указанным идентификатором не найден.");
        }

        var room = new Room
        {
            RoomNumber = request.RoomNumber,
            Floor = request.Floor,
            HasBalcony = request.HasBalcony,
            RoomType = roomType
        };

        var createdRoom = roomService.Add(room);

        return CreatedAtAction(
            nameof(GetRoom),
            new { id = createdRoom.Id },
            ToResponse(createdRoom));
    }

    /// <summary>
    /// Обновляет данные существующего номера
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateRoom(int id, UpdateRoomRequest request)
    {
        var existingRoom = roomService.GetById(id);

        if (existingRoom is null)
        {
            return NotFound();
        }

        var roomType = roomTypeService.GetById(request.RoomTypeId);

        if (roomType is null)
        {
            return BadRequest("Тип номера с указанным идентификатором не найден.");
        }

        var room = new Room
        {
            Id = id,
            RoomNumber = request.RoomNumber,
            Floor = request.Floor,
            HasBalcony = request.HasBalcony,
            RoomType = roomType
        };

        roomService.Update(id, room);

        return NoContent();
    }

    /// <summary>
    /// Удаляет номер по его идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteRoom(int id)
    {
        roomService.Delete(id);

        return NoContent();
    }

    private static RoomResponse ToResponse(Room room) => new()
    {
        Id = room.Id,
        RoomNumber = room.RoomNumber,
        Floor = room.Floor,
        HasBalcony = room.HasBalcony,
        RoomTypeId = room.RoomType.Id,
        RoomCategory = room.RoomType.Category.ToString()
    };
}