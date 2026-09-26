using HotelSystem.Domain.Shared.Enums;
using HotelSystem.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов по данным гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _analyticsService;

    /// <summary>
    /// Инициализирует контроллер аналитики
    /// </summary>
    public AnalyticsController(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    /// <summary>
    /// 1. Вывести информацию о всех клиентах,проживавших в номерах указанного типа, упорядочить по ФИО
    /// </summary>
    [HttpGet("clients-by-room-category/{category}")]
    public IActionResult GetClientsByRoomCategory(RoomCategory category)
    {
        var clients = _analyticsService.GetClientsByRoomCategory(category);

        return Ok(clients);
    }

    /// <summary>
    /// 2. Вывести информацию о номерах, находящихся в текущем бронировании
    /// </summary>
    [HttpGet("occupied-rooms")]
    public IActionResult GetCurrentlyBookedRooms()
    {
        var rooms = _analyticsService.GetCurrentlyBookedRooms();

        return Ok(rooms);
    }

    /// <summary>
    /// 3. Вывести топ 5 наиболее часто бронируемых номеров
    /// </summary>
    [HttpGet("top-booked-rooms")]
    public IActionResult GetTop5MostFrequentlyBookedRooms()
    {
        var rooms = _analyticsService.GetTop5MostFrequentlyBookedRooms();

        return Ok(rooms);
    }

    /// <summary>
    /// 4. Для каждого номера вывести число бронирований
    /// <summary>
    [HttpGet("booking-count-by-room")]
    public IActionResult GetBookingCountForEachRoom()
    {
        var bookingCounts = _analyticsService.GetBookingCountForEachRoom();

        return Ok(bookingCounts);
    }

    /// <summary>
    /// 5.Вывести топ 5 клиентов по суммарной стоимости проживания
    /// </summary>
    [HttpGet("top-clients-by-cost")]
    public IActionResult GetTop5ClientsByTotalCost()
    {
        var clients = _analyticsService.GetTop5ClientsByTotalCost();

        return Ok(clients);
    }
}