using HotelSystem.Application.Contracts.Bookings;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для выполнения CRUD-операций с бронированиями гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BookingsController(
    BookingService bookingService,
    ClientService clientService,
    RoomService roomService) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех бронирований
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<BookingResponse>), StatusCodes.Status200OK)]
    public ActionResult<List<BookingResponse>> GetBookings()
    {
        var bookings = bookingService.GetAll()
            .Select(ToResponse)
            .ToList();

        return Ok(bookings);
    }

    /// <summary>
    /// Возвращает бронирование по его идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<BookingResponse> GetBooking(int id)
    {
        var booking = bookingService.GetById(id);

        if (booking is null)
        {
            return NotFound();
        }

        return Ok(ToResponse(booking));
    }

    /// <summary>
    /// Создаёт новое бронирование
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<BookingResponse> CreateBooking(CreateBookingRequest request)
    {
        var client = clientService.GetById(request.ClientId);

        if (client is null)
        {
            return BadRequest("Клиент с указанным идентификатором не найден.");
        }

        var room = roomService.GetById(request.RoomId);

        if (room is null)
        {
            return BadRequest("Номер с указанным идентификатором не найден.");
        }

        var booking = new Booking
        {
            Client = client,
            Room = room,
            CheckInDate = request.CheckInDate,
            NumberOfDays = request.NumberOfDays
        };

        var createdBooking = bookingService.Add(booking);

        return CreatedAtAction(
            nameof(GetBooking),
            new { id = createdBooking.Id },
            ToResponse(createdBooking));
    }

    /// <summary>
    /// Обновляет данные существующего бронирования
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateBooking(int id, UpdateBookingRequest request)
    {
        var existingBooking = bookingService.GetById(id);

        if (existingBooking is null)
        {
            return NotFound();
        }

        var client = clientService.GetById(request.ClientId);

        if (client is null)
        {
            return BadRequest("Клиент с указанным идентификатором не найден.");
        }

        var room = roomService.GetById(request.RoomId);

        if (room is null)
        {
            return BadRequest("Номер с указанным идентификатором не найден.");
        }

        var booking = new Booking
        {
            Id = id,
            Client = client,
            Room = room,
            CheckInDate = request.CheckInDate,
            NumberOfDays = request.NumberOfDays
        };

        bookingService.Update(id, booking);

        return NoContent();
    }

    /// <summary>
    /// Удаляет бронирование по его идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteBooking(int id)
    {
        bookingService.Delete(id);

        return NoContent();
    }

    private static BookingResponse ToResponse(Booking booking) => new()
    {
        Id = booking.Id,
        ClientId = booking.Client.Id,
        RoomId = booking.Room.Id,
        CheckInDate = booking.CheckInDate,
        NumberOfDays = booking.NumberOfDays,
        TotalCost = booking.TotalCost
    };
}