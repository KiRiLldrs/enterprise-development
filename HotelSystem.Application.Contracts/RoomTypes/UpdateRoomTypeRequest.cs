using HotelSystem.Domain.Shared.Enums;

namespace HotelSystem.Application.Contracts.RoomTypes;

/// <summary>
/// Данные для обновления типа номера
/// </summary>
public class UpdateRoomTypeRequest
{
    public RoomCategory Category { get; init; }
    public double Area { get; init; }
    public int BedCount { get; init; }
    public bool HasBathOrShower { get; init; }
    public decimal PricePerDay { get; init; }
}