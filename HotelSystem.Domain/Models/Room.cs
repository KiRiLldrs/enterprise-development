namespace HotelSystem.Domain.Models;

public class Room
{
    public int RoomNumber { get; set; }

    public int Floor { get; set; }

    public bool HasBalcony { get; set; }

    public required RoomType RoomType { get; set; }
}