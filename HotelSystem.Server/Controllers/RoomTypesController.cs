using HotelSystem.Application.Contracts.RoomTypes;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для управления типами номеров гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoomTypesController(
    RoomTypeService roomTypeService,
    ILogger<RoomTypesController> logger)
    : BaseCrudController<
        RoomType,
        RoomTypeResponse,
        CreateRoomTypeRequest,
        UpdateRoomTypeRequest>(
        roomTypeService,
        logger)
{
    /// <inheritdoc />
    protected override RoomTypeResponse ToResponse(RoomType roomType)
    {
        return new RoomTypeResponse
        {
            Id = roomType.Id,
            Category = roomType.Category,
            Area = roomType.Area,
            BedCount = roomType.BedCount,
            HasBathOrShower = roomType.HasBathOrShower,
            PricePerDay = roomType.PricePerDay
        };
    }

    /// <inheritdoc />
    protected override RoomType MapCreateRequest(
        CreateRoomTypeRequest request)
    {
        return new RoomType
        {
            Category = request.Category,
            Area = request.Area,
            BedCount = request.BedCount,
            HasBathOrShower = request.HasBathOrShower,
            PricePerDay = request.PricePerDay
        };
    }

    /// <inheritdoc />
    protected override RoomType MapUpdateRequest(
        int id,
        UpdateRoomTypeRequest request)
    {
        return new RoomType
        {
            Id = id,
            Category = request.Category,
            Area = request.Area,
            BedCount = request.BedCount,
            HasBathOrShower = request.HasBathOrShower,
            PricePerDay = request.PricePerDay
        };
    }

    /// <inheritdoc />
    protected override int GetEntityId(RoomType roomType) => roomType.Id;
}