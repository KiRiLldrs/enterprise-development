
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий клиентов гостиницы с хранением данных в базе PostgreSQL
/// </summary>
public class EfClientRepository(HotelDbContext context) : IRepository<Client>
{
    /// <inheritdoc />
    public List<Client> GetAll() =>
        context.Clients.ToList();

    /// <inheritdoc />
    public Client? GetById(int id) =>
        context.Clients.FirstOrDefault(client => client.Id == id);

    /// <inheritdoc />
    public Client Add(Client client)
    {
        context.Clients.Add(client);
        context.SaveChanges();

        return client;
    }

    /// <inheritdoc />
    public bool Update(int id, Client client)
    {
        var existingClient = GetById(id);

        if (existingClient is null)
        {
            return false;
        }

        context.Entry(existingClient).CurrentValues.SetValues(client);
        context.SaveChanges();

        return true;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var client = GetById(id);

        if (client is null)
        {
            return false;
        }

        context.Clients.Remove(client);
        context.SaveChanges();

        return true;
    }
}
