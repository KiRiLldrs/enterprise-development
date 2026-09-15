using HotelSystem.Domain.Enums;

namespace HotelSystem.Domain.Models;

public class RoomType
{
    public int Id { get; set; }

    public RoomCategory Category { get; set; }

    public double Area { get; set; }

    public int BedCount { get; set; }

    public bool HasBathOrShower { get; set; }

    public decimal PricePerDay { get; set; }
}