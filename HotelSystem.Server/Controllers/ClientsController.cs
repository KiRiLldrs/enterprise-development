using HotelSystem.Application.Contracts.Clients;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для управления клиентами гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(
    ClientService clientService,
    ILogger<ClientsController> logger)
    : BaseCrudController<Client, ClientResponse, CreateClientRequest, UpdateClientRequest>(
        clientService,
        logger)
{
    /// <inheritdoc />
    protected override ClientResponse ToResponse(Client client)
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

    /// <inheritdoc />
    protected override Client MapCreateRequest(CreateClientRequest request)
    {
        return new Client
        {
            PassportNumber = request.PassportNumber,
            LastName = request.LastName,
            FirstName = request.FirstName,
            Patronymic = request.Patronymic,
            BirthDate = request.BirthDate,
            Citizenship = request.Citizenship
        };
    }

    /// <inheritdoc />
    protected override Client MapUpdateRequest(
        int id,
        UpdateClientRequest request)
    {
        return new Client
        {
            Id = id,
            PassportNumber = request.PassportNumber,
            LastName = request.LastName,
            FirstName = request.FirstName,
            Patronymic = request.Patronymic,
            BirthDate = request.BirthDate,
            Citizenship = request.Citizenship
        };
    }

    /// <inheritdoc />
    protected override int GetEntityId(Client client) => client.Id;
}