namespace HotelSystem.Domain.Models;

public class Room
{
    public int RoomNumber { get; set; }

    public int Floor { get; set; }

    public bool HasBalcony { get; set; }

    public RoomType RoomType { get; set; }
}