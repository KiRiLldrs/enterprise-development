using HotelSystem.Domain.Data;
using HotelSystem.Domain.Models;

namespace HotelSystem.Server.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с бронированиями гостиницы
/// </summary>
public class BookingService
{
    private readonly List<Booking> _bookings;

    /// <summary>
    /// Загружает тестовые данные бронирований
    /// </summary>
    public BookingService()
    {
        _bookings = HotelTestData.Bookings;
    }

    /// <summary>
    /// Возвращает список всех бронирований
    /// </summary>
    public List<Booking> GetAll()
    {
        return _bookings;
    }

    /// <summary>
    /// Возвращает бронирование по его идентификатору
    /// </summary>
    public Booking? GetById(int id)
    {
        return _bookings.FirstOrDefault(booking => booking.Id == id);
    }

    /// <summary>
    /// Добавляет новое бронирование в список
    /// </summary>
    public Booking Add(Booking booking)
    {
        booking.Id = _bookings.Count == 0
            ? 1
            : _bookings.Max(existingBooking => existingBooking.Id) + 1;

        _bookings.Add(booking);

        return booking;
    }

    /// <summary>
    /// Обновляет данные существующего бронирования
    /// </summary>
    public bool Update(int id, Booking booking)
    {
        var existingBooking = GetById(id);

        if (existingBooking is null)
        {
            return false;
        }

        booking.Id = id;

        var index = _bookings.IndexOf(existingBooking);
        _bookings[index] = booking;

        return true;
    }

    /// <summary>
    /// Удаляет бронирование по его идентификатору
    /// </summary>
    public bool Delete(int id)
    {
        var booking = GetById(id);

        if (booking is null)
        {
            return false;
        }

        _bookings.Remove(booking);

        return true;
    }
}