namespace HotelSystem.Domain.Models;

public class Client
{
    public required string PassportNumber { get; set; }

    public required string FullName { get; set; }

    public DateTime BirthDate { get; set; }

    public required string Citizenship { get; set; }
}