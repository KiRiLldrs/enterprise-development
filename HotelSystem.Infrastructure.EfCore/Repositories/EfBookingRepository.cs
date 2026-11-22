
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий бронирований гостиницы с хранением данных в базе PostgreSQL
/// </summary>
public class EfBookingRepository(HotelDbContext context) : IRepository<Booking>
{
    /// <inheritdoc />
    public List<Booking> GetAll() =>
        context.Bookings
            .Include(booking => booking.Client)
            .Include(booking => booking.Room)
                .ThenInclude(room => room.RoomType)
            .ToList();

    /// <inheritdoc />
    public Booking? GetById(int id) =>
        context.Bookings
            .Include(booking => booking.Client)
            .Include(booking => booking.Room)
                .ThenInclude(room => room.RoomType)
            .FirstOrDefault(booking => booking.Id == id);

    /// <inheritdoc />
    public Booking Add(Booking booking)
    {
        context.Bookings.Add(booking);
        context.SaveChanges();

        return booking;
    }

    /// <inheritdoc />
    public bool Update(int id, Booking booking)
    {
        var existingBooking = GetById(id);

        if (existingBooking is null)
        {
            return false;
        }

        existingBooking.Client = booking.Client;
        existingBooking.Room = booking.Room;
        existingBooking.CheckInDate = booking.CheckInDate;
        existingBooking.NumberOfDays = booking.NumberOfDays;

        context.SaveChanges();

        return true;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var booking = GetById(id);

        if (booking is null)
        {
            return false;
        }

        context.Bookings.Remove(booking);
        context.SaveChanges();

        return true;
    }
}
