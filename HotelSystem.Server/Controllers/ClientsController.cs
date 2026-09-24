using HotelSystem.Domain.Models;
using HotelSystem.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientsController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public IActionResult GetClients()
    {
        return Ok(_clientService.GetAll());
    }

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

    [HttpPost]
    public IActionResult CreateClient(Client client)
    {
        var createdClient = _clientService.Add(client);

        return CreatedAtAction(
            nameof(GetClient),
            new { id = createdClient.Id },
            createdClient);
    }

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