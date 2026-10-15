namespace HotelSystem.Application.Contracts.Clients;

/// <summary>
/// Контракт ответа с информацией о клиенте
/// </summary>
public class ClientResponse
{
    public int Id { get; init; }

    public required string PassportNumber { get; init; }

    public required string LastName { get; init; }

    public required string FirstName { get; init; }

    public string? Patronymic { get; init; }

    public DateTime BirthDate { get; init; }

    public required string Citizenship { get; init; }
}