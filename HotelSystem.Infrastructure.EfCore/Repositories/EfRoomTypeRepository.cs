
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.EfCore.Data;

namespace HotelSystem.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий типов номеров гостиницы с хранением данных в базе PostgreSQL
/// </summary>
public class EfRoomTypeRepository(HotelDbContext context) : IRepository<RoomType>
{
    /// <inheritdoc />
    public List<RoomType> GetAll() =>
        context.RoomTypes.ToList();

    /// <inheritdoc />
    public RoomType? GetById(int id) =>
        context.RoomTypes.FirstOrDefault(roomType => roomType.Id == id);

    /// <inheritdoc />
    public RoomType Add(RoomType roomType)
    {
        context.RoomTypes.Add(roomType);
        context.SaveChanges();

        return roomType;
    }

    /// <inheritdoc />
    public bool Update(int id, RoomType roomType)
    {
        var existingRoomType = GetById(id);

        if (existingRoomType is null)
        {
            return false;
        }

        context.Entry(existingRoomType).CurrentValues.SetValues(roomType);
        context.SaveChanges();

        return true;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var roomType = GetById(id);

        if (roomType is null)
        {
            return false;
        }

        context.RoomTypes.Remove(roomType);
        context.SaveChanges();

        return true;
    }
}
