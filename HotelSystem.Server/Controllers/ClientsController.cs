using HotelSystem.Domain.Models;
using HotelSystem.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для выполнения CRUD-операций с клиентами гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ClientService _clientService;

    /// <summary>
    /// Инициализирует контроллер клиентов
    /// </summary>
    public ClientsController(ClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    [HttpGet]
    public IActionResult GetClients()
    {
        return Ok(_clientService.GetAll());
    }

    /// <summary>
    /// Возвращает клиента по его идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public IActionResult GetClient(int id)
    {
        var client = _clientService.GetById(id);

        if (client is null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    /// <summary>
    /// Создаёт нового клиента
    /// </summary>
    [HttpPost]
    public IActionResult CreateClient(Client client)
    {
        var createdClient = _clientService.Add(client);

        return CreatedAtAction(
            nameof(GetClient),
            new { id = createdClient.Id },
            createdClient);
    }

    /// <summary>
    /// Обновляет данные существующего клиента
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult UpdateClient(int id, Client client)
    {
        var updated = _clientService.Update(id, client);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет клиента по его идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteClient(int id)
    {
        var deleted = _clientService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}