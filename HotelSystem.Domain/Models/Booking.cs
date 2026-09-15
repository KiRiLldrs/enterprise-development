namespace HotelSystem.Domain.Models;

public class Booking
{
    public required Client Client { get; set; }

    public required Room Room { get; set; }

    public DateTime CheckInDate { get; set; }

    public int NumberOfDays { get; set; }
}