using HotelSystem.Domain.Models;

namespace HotelSystem.Application.Repositories;

public interface IRoomRepository
{
    public List<Room> GetAll();

    public Room? GetById(int id);

    public Room Add(Room room);

    public bool Update(int id, Room room);

    public bool Delete(int id);
}