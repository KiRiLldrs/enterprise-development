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
    RoomService roomService,
    ILogger<BookingsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех бронирований
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<BookingResponse>), StatusCodes.Status200OK)]
    public ActionResult<List<BookingResponse>> GetBookings()
    {
        logger.LogInformation("Получение списка бронирований");

        var bookings = bookingService.GetAll()
            .Select(ToResponse)
            .ToList();

        return Ok(bookings);
    }

    /// <summary>
    /// Возвращает бронирование по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<BookingResponse> GetBooking(int id)
    {
        logger.LogInformation(
            "Поиск бронирования с идентификатором {BookingId}",
            id);

        var booking = bookingService.GetById(id);

        if (booking is null)
        {
            logger.LogWarning(
                "Бронирование с идентификатором {BookingId} не найдено",
                id);

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
        logger.LogInformation("Создание нового бронирования");

        var client = clientService.GetById(request.ClientId);

        if (client is null)
        {
            logger.LogWarning(
                "Не удалось создать бронирование: клиент с идентификатором {ClientId} не найден",
                request.ClientId);

            return BadRequest("Клиент с указанным идентификатором не найден");
        }

        var room = roomService.GetById(request.RoomId);

        if (room is null)
        {
            logger.LogWarning(
                "Не удалось создать бронирование: номер с идентификатором {RoomId} не найден",
                request.RoomId);

            return BadRequest("Номер с указанным идентификатором не найден");
        }

        var booking = new Booking
        {
            Client = client,
            Room = room,
            CheckInDate = request.CheckInDate,
            NumberOfDays = request.NumberOfDays
        };

        var createdBooking = bookingService.Add(booking);

        logger.LogInformation(
            "Создано бронирование с идентификатором {BookingId}",
            createdBooking.Id);

        return CreatedAtAction(
            nameof(GetBooking),
            new { id = createdBooking.Id },
            ToResponse(createdBooking));
    }

    /// <summary>
    /// Обновляет существующее бронирование
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateBooking(int id, UpdateBookingRequest request)
    {
        logger.LogInformation(
            "Обновление бронирования с идентификатором {BookingId}",
            id);

        var existingBooking = bookingService.GetById(id);

        if (existingBooking is null)
        {
            logger.LogWarning(
                "Не удалось обновить бронирование с идентификатором {BookingId}: бронирование не найдено",
                id);

            return NotFound();
        }

        var client = clientService.GetById(request.ClientId);

        if (client is null)
        {
            logger.LogWarning(
                "Не удалось обновить бронирование: клиент с идентификатором {ClientId} не найден",
                request.ClientId);

            return BadRequest("Клиент с указанным идентификатором не найден");
        }

        var room = roomService.GetById(request.RoomId);

        if (room is null)
        {
            logger.LogWarning(
                "Не удалось обновить бронирование: номер с идентификатором {RoomId} не найден",
                request.RoomId);

            return BadRequest("Номер с указанным идентификатором не найден");
        }

        var booking = new Booking
        {
            Id = id,
            Client = client,
            Room = room,
            CheckInDate = request.CheckInDate,
            NumberOfDays = request.NumberOfDays
        };

        var updated = bookingService.Update(id, booking);

        if (!updated)
        {
            logger.LogWarning(
                "Не удалось обновить бронирование с идентификатором {BookingId}",
                id);

            return NotFound();
        }

        logger.LogInformation(
            "Бронирование с идентификатором {BookingId} обновлено",
            id);

        return NoContent();
    }

    /// <summary>
    /// Удаляет бронирование по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteBooking(int id)
    {
        logger.LogInformation(
            "Удаление бронирования с идентификатором {BookingId}",
            id);

        var deleted = bookingService.Delete(id);

        if (deleted)
        {
            logger.LogInformation(
                "Бронирование с идентификатором {BookingId} удалено",
                id);
        }
        else
        {
            logger.LogWarning(
                "Бронирование с идентификатором {BookingId} не найдено при удалении",
                id);
        }

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