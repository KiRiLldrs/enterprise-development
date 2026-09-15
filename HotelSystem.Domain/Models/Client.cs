namespace HotelSystem.Domain.Models;

/// <summary>
/// Клиент гостиницы
/// </summary>
public class Client
{
    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Гражданство
    /// </summary>
    public required string Citizenship { get; set; }
}
