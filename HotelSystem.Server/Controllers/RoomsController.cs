using HotelSystem.Application.Contracts.Rooms;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для управления номерами гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomsController(
    RoomService roomService,
    RoomTypeService roomTypeService,
    ILogger<RoomsController> logger)
    : BaseCrudController<
        Room,
        RoomResponse,
        CreateRoomRequest,
        UpdateRoomRequest>(
        roomService,
        logger)
{
    /// <inheritdoc />
    protected override RoomResponse ToResponse(Room room)
    {
        return new RoomResponse
        {
            Id = room.Id,
            RoomNumber = room.RoomNumber,
            Floor = room.Floor,
            HasBalcony = room.HasBalcony,
            RoomTypeId = room.RoomType.Id,
            RoomCategory = room.RoomType.Category.ToString()
        };
    }

    /// <inheritdoc />
    protected override Room MapCreateRequest(CreateRoomRequest request)
    {
        var roomType = roomTypeService.GetById(request.RoomTypeId)!;

        return new Room
        {
            RoomNumber = request.RoomNumber,
            Floor = request.Floor,
            HasBalcony = request.HasBalcony,
            RoomType = roomType
        };
    }

    /// <inheritdoc />
    protected override Room MapUpdateRequest(
        int id,
        UpdateRoomRequest request)
    {
        var roomType = roomTypeService.GetById(request.RoomTypeId)!;

        return new Room
        {
            Id = id,
            RoomNumber = request.RoomNumber,
            Floor = request.Floor,
            HasBalcony = request.HasBalcony,
            RoomType = roomType
        };
    }

    /// <inheritdoc />
    protected override int GetEntityId(Room room) => room.Id;

    /// <inheritdoc />
    protected override IActionResult? ValidateCreateRequest(
        CreateRoomRequest request)
    {
        if (roomTypeService.GetById(request.RoomTypeId) is null)
        {
            logger.LogWarning(
                "Не удалось создать номер: тип номера с ID {RoomTypeId} не найден",
                request.RoomTypeId);

            return BadRequest(
                "Тип номера с указанным идентификатором не найден");
        }

        return null;
    }

    /// <inheritdoc />
    protected override IActionResult? ValidateUpdateRequest(
        int id,
        UpdateRoomRequest request)
    {
        if (roomTypeService.GetById(request.RoomTypeId) is null)
        {
            logger.LogWarning(
                "Не удалось обновить номер: тип номера с ID {RoomTypeId} не найден",
                request.RoomTypeId);

            return BadRequest(
                "Тип номера с указанным идентификатором не найден");
        }

        return null;
    }
}