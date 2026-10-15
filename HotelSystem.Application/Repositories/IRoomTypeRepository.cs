using HotelSystem.Domain.Models;

namespace HotelSystem.Application.Repositories;

public interface IRoomTypeRepository
{
    public List<RoomType> GetAll();

    public RoomType? GetById(int id);

    public RoomType Add(RoomType roomType);

    public bool Update(int id, RoomType roomType);

    public bool Delete(int id);
}