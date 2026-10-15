using HotelSystem.Application.Repositories;
using HotelSystem.Domain.Models;
using HotelSystem.Infrastructure.Storage;

namespace HotelSystem.Infrastructure.Repositories;

/// <summary>
/// Репозиторий клиентов гостиницы с хранением данных в памяти
/// </summary>
public class InMemoryClientRepository(HotelInMemoryStorage storage)
    : IClientRepository
{
    public List<Client> GetAll() => storage.Clients;

    public Client? GetById(int id) =>
        storage.Clients.FirstOrDefault(client => client.Id == id);

    public Client Add(Client client)
    {
        client.Id = storage.Clients.Count == 0
            ? 1
            : storage.Clients.Max(existing => existing.Id) + 1;

        storage.Clients.Add(client);

        return client;
    }

    public bool Update(int id, Client client)
    {
        var index = storage.Clients.FindIndex(existing => existing.Id == id);

        if (index == -1)
        {
            return false;
        }

        client.Id = id;
        storage.Clients[index] = client;

        return true;
    }

    public bool Delete(int id)
    {
        var client = GetById(id);

        return client is not null && storage.Clients.Remove(client);
    }
}