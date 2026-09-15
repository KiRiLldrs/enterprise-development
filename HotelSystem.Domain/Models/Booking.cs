namespace HotelSystem.Domain.Models;

/// <summary>
/// Бронирование номера клиентом
/// </summary>
public class Booking
{
    /// <summary>
    /// Клиент, оформивший бронирование
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Забронированный номер
    /// </summary>
    public required Room Room { get; set; }

    /// <summary>
    /// Дата заселения клиента
    /// </summary>
    public DateTime CheckInDate { get; set; }

    /// <summary>
    /// Количество дней проживания
    /// </summary>
    public int NumberOfDays { get; set; }
}
