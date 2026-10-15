namespace HotelSystem.Application.Contracts.Bookings;

/// <summary>
/// Данные бронирования, возвращаемые API
/// </summary>
public class BookingResponse
{
    public int Id { get; init; }

    public int ClientId { get; init; }

    public int RoomId { get; init; }

    public DateTime CheckInDate { get; init; }

    public int NumberOfDays { get; init; }

    public decimal TotalCost { get; init; }
}