using HotelSystem.Domain.Models;
using HotelSystem.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для выполнения CRUD-операций с бронированиями гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly BookingService _bookingService;

    /// <summary>
    /// Инициализирует контроллер бронирований
    /// </summary>
    public BookingsController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    /// <summary>
    /// Возвращает список всех бронирований
    /// </summary>
    [HttpGet]
    public IActionResult GetBookings()
    {
        return Ok(_bookingService.GetAll());
    }

    /// <summary>
    /// Возвращает бронирование по его идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    public IActionResult GetBooking(int id)
    {
        var booking = _bookingService.GetById(id);

        if (booking is null)
        {
            return NotFound();
        }

        return Ok(booking);
    }

    /// <summary>
    /// Создаёт новое бронирование
    /// </summary>
    [HttpPost]
    public IActionResult CreateBooking(Booking booking)
    {
        var createdBooking = _bookingService.Add(booking);

        return CreatedAtAction(
            nameof(GetBooking),
            new { id = createdBooking.Id },
            createdBooking);
    }

    /// <summary>
    /// Обновляет данные существующего бронирования
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult UpdateBooking(int id, Booking booking)
    {
        var updated = _bookingService.Update(id, booking);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Удаляет бронирование по его идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteBooking(int id)
    {
        var deleted = _bookingService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}