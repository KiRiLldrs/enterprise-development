using HotelSystem.Domain.Shared.Enums;

namespace HotelSystem.Application.Contracts.RoomTypes;

/// <summary>
/// Данные типа номера, возвращаемые через API
/// </summary>
public class RoomTypeResponse
{
    public int Id { get; init; }
    public RoomCategory Category { get; init; }
    public double Area { get; init; }
    public int BedCount { get; init; }
    public bool HasBathOrShower { get; init; }
    public decimal PricePerDay { get; init; }
}