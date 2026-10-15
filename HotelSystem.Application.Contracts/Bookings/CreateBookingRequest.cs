namespace HotelSystem.Application.Contracts.Bookings;

/// <summary>
/// Данные для создания бронирования
/// </summary>
public class CreateBookingRequest
{
    public int ClientId { get; init; }

    public int RoomId { get; init; }

    public DateTime CheckInDate { get; init; }

    public int NumberOfDays { get; init; }
}