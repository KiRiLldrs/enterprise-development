using HotelSystem.Application.Repositories;
using HotelSystem.Domain.Models;

namespace HotelSystem.Application.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с бронированиями
/// </summary>
public class BookingService(IBookingRepository bookingRepository)
{
    /// <summary>
    /// Возвращает все бронирования
    /// </summary>
    public List<Booking> GetAll() =>
        bookingRepository.GetAll();

    /// <summary>
    /// Возвращает бронирование по идентификатору
    /// </summary>
    public Booking? GetById(int id) =>
        bookingRepository.GetById(id);

    /// <summary>
    /// Добавляет новое бронирование
    /// </summary>
    public Booking Add(Booking booking) =>
        bookingRepository.Add(booking);

    /// <summary>
    /// Обновляет бронирование
    /// </summary>
    public bool Update(int id, Booking booking) =>
        bookingRepository.Update(id, booking);

    /// <summary>
    /// Удаляет бронирование
    /// </summary>
    public bool Delete(int id) =>
        bookingRepository.Delete(id);
}