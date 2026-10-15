using HotelSystem.Application.Contracts.RoomTypes;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для выполнения CRUD-операций с типами номеров гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomTypesController(
    RoomTypeService roomTypeService,
    ILogger<RoomTypesController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех типов номеров
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<RoomTypeResponse>), StatusCodes.Status200OK)]
    public ActionResult<List<RoomTypeResponse>> GetRoomTypes()
    {
        logger.LogInformation("Получение списка типов номеров");

        var roomTypes = roomTypeService.GetAll()
            .Select(ToResponse)
            .ToList();

        return Ok(roomTypes);
    }

    /// <summary>
    /// Возвращает тип номера по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoomTypeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<RoomTypeResponse> GetRoomType(int id)
    {
        logger.LogInformation(
            "Поиск типа номера с идентификатором {RoomTypeId}",
            id);

        var roomType = roomTypeService.GetById(id);

        if (roomType is null)
        {
            logger.LogWarning(
                "Тип номера с идентификатором {RoomTypeId} не найден",
                id);

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
        logger.LogInformation("Создание нового типа номера");

        var roomType = new RoomType
        {
            Category = request.Category,
            Area = request.Area,
            BedCount = request.BedCount,
            HasBathOrShower = request.HasBathOrShower,
            PricePerDay = request.PricePerDay
        };

        var createdRoomType = roomTypeService.Add(roomType);

        logger.LogInformation(
            "Создан тип номера с идентификатором {RoomTypeId}",
            createdRoomType.Id);

        return CreatedAtAction(
            nameof(GetRoomType),
            new { id = createdRoomType.Id },
            ToResponse(createdRoomType));
    }

    /// <summary>
    /// Обновляет существующий тип номера
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateRoomType(int id, UpdateRoomTypeRequest request)
    {
        logger.LogInformation(
            "Обновление типа номера с идентификатором {RoomTypeId}",
            id);

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
            logger.LogWarning(
                "Не удалось обновить тип номера с идентификатором {RoomTypeId}: тип номера не найден",
                id);

            return NotFound();
        }

        logger.LogInformation(
            "Тип номера с идентификатором {RoomTypeId} обновлён",
            id);

        return NoContent();
    }

    /// <summary>
    /// Удаляет тип номера по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteRoomType(int id)
    {
        logger.LogInformation(
            "Удаление типа номера с идентификатором {RoomTypeId}",
            id);

        var deleted = roomTypeService.Delete(id);

        if (deleted)
        {
            logger.LogInformation(
                "Тип номера с идентификатором {RoomTypeId} удалён",
                id);
        }
        else
        {
            logger.LogWarning(
                "Тип номера с идентификатором {RoomTypeId} не найден при удалении",
                id);
        }

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