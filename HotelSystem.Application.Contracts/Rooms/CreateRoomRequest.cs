namespace HotelSystem.Application.Contracts.Rooms;

/// <summary>
/// Данные для создания номера гостиницы
/// </summary>
public class CreateRoomRequest
{
    public int RoomNumber { get; init; }

    public int Floor { get; init; }

    public bool HasBalcony { get; init; }

    public int RoomTypeId { get; init; }
}