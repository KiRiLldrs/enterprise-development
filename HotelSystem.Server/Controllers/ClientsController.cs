using HotelSystem.Application.Contracts.Clients;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для выполнения CRUD-операций с клиентами гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(
    ClientService clientService,
    ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientResponse>), StatusCodes.Status200OK)]
    public ActionResult<List<ClientResponse>> GetClients()
    {
        logger.LogInformation("Получение списка клиентов");

        var clients = clientService.GetAll()
            .Select(ToResponse)
            .ToList();

        return Ok(clients);
    }

    /// <summary>
    /// Возвращает клиента по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ClientResponse> GetClient(int id)
    {
        logger.LogInformation(
            "Поиск клиента с идентификатором {ClientId}.",
            id);

        var client = clientService.GetById(id);

        if (client is null)
        {
            logger.LogWarning(
                "Клиент с идентификатором {ClientId} не найден",
                id);

            return NotFound();
        }

        return Ok(ToResponse(client));
    }

    /// <summary>
    /// Создаёт нового клиента
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ClientResponse> CreateClient(CreateClientRequest request)
    {
        logger.LogInformation("Создание нового клиента");

        var client = new Client
        {
            PassportNumber = request.PassportNumber,
            LastName = request.LastName,
            FirstName = request.FirstName,
            Patronymic = request.Patronymic,
            BirthDate = request.BirthDate,
            Citizenship = request.Citizenship
        };

        var createdClient = clientService.Add(client);

        logger.LogInformation(
            "Создан клиент с идентификатором {ClientId}",
            createdClient.Id);

        return CreatedAtAction(
            nameof(GetClient),
            new { id = createdClient.Id },
            ToResponse(createdClient));
    }

    /// <summary>
    /// Обновляет данные существующего клиента
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateClient(int id, UpdateClientRequest request)
    {
        logger.LogInformation(
            "Обновление клиента с идентификатором {ClientId}",
            id);

        var client = new Client
        {
            PassportNumber = request.PassportNumber,
            LastName = request.LastName,
            FirstName = request.FirstName,
            Patronymic = request.Patronymic,
            BirthDate = request.BirthDate,
            Citizenship = request.Citizenship
        };

        var updated = clientService.Update(id, client);

        if (!updated)
        {
            logger.LogWarning(
                "Не удалось обновить клиента с идентификатором {ClientId}: клиент не найден",
                id);

            return NotFound();
        }

        logger.LogInformation(
            "Клиент с идентификатором {ClientId} обновлён",
            id);

        return NoContent();
    }

    /// <summary>
    /// Удаляет клиента по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteClient(int id)
    {
        logger.LogInformation(
            "Удаление клиента с идентификатором {ClientId}",
            id);

        var deleted = clientService.Delete(id);

        if (deleted)
        {
            logger.LogInformation(
                "Клиент с идентификатором {ClientId} удалён",
                id);
        }
        else
        {
            logger.LogWarning(
                "Клиент с идентификатором {ClientId} не найден при удалении",
                id);
        }

        return NoContent();
    }

    private static ClientResponse ToResponse(Client client) => new()
    {
        Id = client.Id,
        PassportNumber = client.PassportNumber,
        LastName = client.LastName,
        FirstName = client.FirstName,
        Patronymic = client.Patronymic,
        BirthDate = client.BirthDate,
        Citizenship = client.Citizenship
    };
}