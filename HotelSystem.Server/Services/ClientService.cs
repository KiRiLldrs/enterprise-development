using HotelSystem.Domain.Data;
using HotelSystem.Domain.Models;

namespace HotelSystem.Server.Services;

public class ClientService
{
    private readonly List<Client> _clients;

    public ClientService()
    {
        _clients = HotelTestData.Clients;
    }

    public List<Client> GetAll()
    {
        return _clients;
    }

    public Client? GetById(int id)
    {
        return _clients.FirstOrDefault(client => client.Id == id);
    }

    public Client Add(Client client)
    {
        client.Id = _clients.Count == 0
            ? 1
            : _clients.Max(existingClient => existingClient.Id) + 1;

        _clients.Add(client);

        return client;
    }

    public bool Update(int id, Client client)
    {
        var existingClient = GetById(id);

        if (existingClient is null)
        {
            return false;
        }

        client.Id = id;

        var index = _clients.IndexOf(existingClient);
        _clients[index] = client;

        return true;
    }

    public bool Delete(int id)
    {
        var client = GetById(id);

        if (client is null)
        {
            return false;
        }

        _clients.Remove(client);

        return true;
    }
}