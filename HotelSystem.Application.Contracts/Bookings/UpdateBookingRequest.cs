namespace HotelSystem.Application.Contracts.Bookings;

/// <summary>
/// Данные для обновления бронирования
/// </summary>
public class UpdateBookingRequest
{
    public int ClientId { get; init; }

    public int RoomId { get; init; }

    public DateTime CheckInDate { get; init; }

    public int NumberOfDays { get; init; }
}