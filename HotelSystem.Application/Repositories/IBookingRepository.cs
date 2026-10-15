using HotelSystem.Domain.Models;

namespace HotelSystem.Application.Repositories;

public interface IBookingRepository
{
    public List<Booking> GetAll();

    public Booking? GetById(int id);

    public Booking Add(Booking booking);

    public bool Update(int id, Booking booking);

    public bool Delete(int id);
}