using HotelSystem.Application.Contracts.RoomTypes;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для выполнения CRUD-операций с типами номеров гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomTypesController(RoomTypeService roomTypeService) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех типов номеров
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<RoomTypeResponse>), StatusCodes.Status200OK)]
    public ActionResult<List<RoomTypeResponse>> GetRoomTypes()
    {
        var roomTypes = roomTypeService.GetAll()
            .Select(ToResponse)
            .ToList();

        return Ok(roomTypes);
    }

    /// <summary>
    /// Возвращает тип номера по его идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoomTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RoomTypeResponse> GetRoomType(int id)
    {
        var roomType = roomTypeService.GetById(id);

        if (roomType is null)
        {
            return NotFound();
        }

        return Ok(ToResponse(roomType));
    }

    /// <summary>
    /// Создаёт новый тип номера
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RoomTypeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<RoomTypeResponse> CreateRoomType(CreateRoomTypeRequest request)
    {
        var roomType = new RoomType
        {
            Category = request.Category,
            Area = request.Area,
            BedCount = request.BedCount,
            HasBathOrShower = request.HasBathOrShower,
            PricePerDay = request.PricePerDay
        };

        var createdRoomType = roomTypeService.Add(roomType);

        return CreatedAtAction(
            nameof(GetRoomType),
            new { id = createdRoomType.Id },
            ToResponse(createdRoomType));
    }

    /// <summary>
    /// Обновляет данные существующего типа номера
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateRoomType(int id, UpdateRoomTypeRequest request)
    {
        var roomType = new RoomType
        {
            Category = request.Category,
            Area = request.Area,
            BedCount = request.BedCount,
            HasBathOrShower = request.HasBathOrShower,
            PricePerDay = request.PricePerDay
        };

        var updated = roomTypeService.Update(id, roomType);

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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteRoomType(int id)
    {
        roomTypeService.Delete(id);
        return NoContent();
    }

    private static RoomTypeResponse ToResponse(RoomType roomType) => new()
    {
        Id = roomType.Id,
        Category = roomType.Category,
        Area = roomType.Area,
        BedCount = roomType.BedCount,
        HasBathOrShower = roomType.HasBathOrShower,
        PricePerDay = roomType.PricePerDay
    };
}