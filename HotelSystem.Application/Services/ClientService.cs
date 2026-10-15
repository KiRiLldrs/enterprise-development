using HotelSystem.Application.Repositories;
using HotelSystem.Domain.Models;

namespace HotelSystem.Application.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с клиентами гостиницы
/// </summary>
public class ClientService(IClientRepository clientRepository)
{
    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    public List<Client> GetAll()
    {
        return clientRepository.GetAll();
    }

    /// <summary>
    /// Возвращает клиента по идентификатору
    /// </summary>
    public Client? GetById(int id)
    {
        return clientRepository.GetById(id);
    }

    /// <summary>
    /// Добавляет нового клиента
    /// </summary>
    public Client Add(Client client)
    {
        return clientRepository.Add(client);
    }

    /// <summary>
    /// Обновляет данные существующего клиента
    /// </summary>
    public bool Update(int id, Client client)
    {
        return clientRepository.Update(id, client);
    }

    /// <summary>
    /// Удаляет клиента по идентификатору
    /// </summary>
    public bool Delete(int id)
    {
        return clientRepository.Delete(id);
    }
}