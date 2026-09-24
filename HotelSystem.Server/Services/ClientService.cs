using HotelSystem.Domain.Data;
using HotelSystem.Domain.Models;

namespace HotelSystem.Server.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с клиентами гостиницы
/// </summary>
public class ClientService
{
    private readonly List<Client> _clients;

    /// <summary>
    /// Загружает тестовые данные клиентов
    /// </summary>
    public ClientService()
    {
        _clients = HotelTestData.Clients;
    }

    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    public List<Client> GetAll()
    {
        return _clients;
    }

    /// <summary>
    /// Возвращает клиента по его идентификатору
    /// </summary>
    public Client? GetById(int id)
    {
        return _clients.FirstOrDefault(client => client.Id == id);
    }

    /// <summary>
    /// Добавляет нового клиента в список
    /// </summary>
    public Client Add(Client client)
    {
        client.Id = _clients.Count == 0
            ? 1
            : _clients.Max(existingClient => existingClient.Id) + 1;

        _clients.Add(client);

        return client;
    }

    /// <summary>
    /// Обновляет данные существующего клиента
    /// </summary>
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

    /// <summary>
    /// Удаляет клиента по его идентификатору
    /// </summary>
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