namespace HotelSystem.Application.Contracts.Rooms;

/// <summary>
/// Данные номера гостиницы, возвращаемые API
/// </summary>
public class RoomResponse
{
    public int Id { get; init; }

    public int RoomNumber { get; init; }

    public int Floor { get; init; }

    public bool HasBalcony { get; init; }

    public int RoomTypeId { get; init; }

    public string RoomCategory { get; init; } = string.Empty;
}