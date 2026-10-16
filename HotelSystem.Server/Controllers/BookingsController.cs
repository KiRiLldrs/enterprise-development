using HotelSystem.Application.Contracts.Bookings;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Контроллер для управления бронированиями гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BookingsController(
    BookingService bookingService,
    ClientService clientService,
    RoomService roomService,
    ILogger<BookingsController> logger)
    : BaseCrudController<
        Booking,
        BookingResponse,
        CreateBookingRequest,
        UpdateBookingRequest>(
        bookingService,
        logger)
{
    /// <inheritdoc />
    protected override BookingResponse ToResponse(Booking booking)
    {
        return new BookingResponse
        {
            Id = booking.Id,
            ClientId = booking.Client.Id,
            RoomId = booking.Room.Id,
            CheckInDate = booking.CheckInDate,
            NumberOfDays = booking.NumberOfDays,
            TotalCost = booking.TotalCost
        };
    }

    /// <inheritdoc />
    protected override Booking MapCreateRequest(
        CreateBookingRequest request)
    {
        var client = clientService.GetById(request.ClientId)!;
        var room = roomService.GetById(request.RoomId)!;

        return new Booking
        {
            Client = client,
            Room = room,
            CheckInDate = request.CheckInDate,
            NumberOfDays = request.NumberOfDays
        };
    }

    /// <inheritdoc />
    protected override Booking MapUpdateRequest(
        int id,
        UpdateBookingRequest request)
    {
        var client = clientService.GetById(request.ClientId)!;
        var room = roomService.GetById(request.RoomId)!;

        return new Booking
        {
            Id = id,
            Client = client,
            Room = room,
            CheckInDate = request.CheckInDate,
            NumberOfDays = request.NumberOfDays
        };
    }

    /// <inheritdoc />
    protected override int GetEntityId(Booking booking) => booking.Id;

    /// <inheritdoc />
    protected override IActionResult? ValidateCreateRequest(
        CreateBookingRequest request)
    {
        if (clientService.GetById(request.ClientId) is null)
        {
            logger.LogWarning(
                "Не удалось создать бронирование: клиент с ID {ClientId} не найден",
                request.ClientId);

            return BadRequest(
                "Клиент с указанным идентификатором не найден");
        }

        if (roomService.GetById(request.RoomId) is null)
        {
            logger.LogWarning(
                "Не удалось создать бронирование: номер с ID {RoomId} не найден",
                request.RoomId);

            return BadRequest(
                "Номер с указанным идентификатором не найден");
        }

        return null;
    }

    /// <inheritdoc />
    protected override IActionResult? ValidateUpdateRequest(
        int id,
        UpdateBookingRequest request)
    {
        if (clientService.GetById(request.ClientId) is null)
        {
            logger.LogWarning(
                "Не удалось обновить бронирование: клиент с ID {ClientId} не найден",
                request.ClientId);

            return BadRequest(
                "Клиент с указанным идентификатором не найден");
        }

        if (roomService.GetById(request.RoomId) is null)
        {
            logger.LogWarning(
                "Не удалось обновить бронирование: номер с ID {RoomId} не найден",
                request.RoomId);

            return BadRequest(
                "Номер с указанным идентификатором не найден");
        }

        return null;
    }
}