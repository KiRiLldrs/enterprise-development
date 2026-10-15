using HotelSystem.Application.Contracts.Clients;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для выполнения CRUD-операций с клиентами гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(ClientService clientService) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientResponse>), StatusCodes.Status200OK)]
    public ActionResult<List<ClientResponse>> GetClients()
    {
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
        var client = clientService.GetById(id);

        if (client is null)
        {
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
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет клиента по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteClient(int id)
    {
        clientService.Delete(id);

        return NoContent();
    }

    private static ClientResponse ToResponse(Client client)
    {
        return new ClientResponse
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
}