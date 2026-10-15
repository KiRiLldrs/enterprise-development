using HotelSystem.Domain.Models;

namespace HotelSystem.Application.Repositories;

public interface IClientRepository
{
    public List<Client> GetAll();

    public Client? GetById(int id);

    public Client Add(Client client);

    public bool Update(int id, Client client);

    public bool Delete(int id);
}
