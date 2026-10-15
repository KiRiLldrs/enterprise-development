namespace HotelSystem.Application.Contracts.Clients;

/// <summary>
/// Контракт запроса на обновление клиента
/// </summary>
public class UpdateClientRequest
{
    public required string PassportNumber { get; init; }

    public required string LastName { get; init; }

    public required string FirstName { get; init; }

    public string? Patronymic { get; init; }

    public DateTime BirthDate { get; init; }

    public required string Citizenship { get; init; }
}