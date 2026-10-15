using HotelSystem.Application.Contracts.Analytics;
using HotelSystem.Application.Contracts.Clients;
using HotelSystem.Application.Contracts.Rooms;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов по данным гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    AnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// 1. Выводит клиентов, проживавших в номерах указанного типа, упорядоченных по ФИО
    /// </summary>
    [HttpGet("clients-by-room-category/{category}")]
    [ProducesResponseType(typeof(List<ClientResponse>), StatusCodes.Status200OK)]
    public ActionResult<List<ClientResponse>> GetClientsByRoomCategory(
        RoomCategory category)
    {
        logger.LogInformation(
            "Получен запрос клиентов по категории номера {Category}",
            category);

        var clients = analyticsService.GetClientsByRoomCategory(category)
            .Select(client => new ClientResponse
            {
                Id = client.Id,
                PassportNumber = client.PassportNumber,
                LastName = client.LastName,
                FirstName = client.FirstName,
                Patronymic = client.Patronymic,
                BirthDate = client.BirthDate,
                Citizenship = client.Citizenship
            })
            .ToList();

        return Ok(clients);
    }

    /// <summary>
    /// 2. Выводит номера, занятые на дату проверки
    /// </summary>
    [HttpGet("occupied-rooms")]
    [ProducesResponseType(typeof(List<RoomResponse>), StatusCodes.Status200OK)]
    public ActionResult<List<RoomResponse>> GetCurrentlyBookedRooms()
    {
        logger.LogInformation("Получен запрос занятых номеров");

        var rooms = analyticsService.GetCurrentlyBookedRooms()
            .Select(room => new RoomResponse
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Floor = room.Floor,
                HasBalcony = room.HasBalcony,
                RoomTypeId = room.RoomType.Id,
                RoomCategory = room.RoomType.Category.ToString()
            })
            .ToList();

        return Ok(rooms);
    }

    /// <summary>
    /// 3. Выводит топ-5 наиболее часто бронируемых номеров
    /// </summary>
    [HttpGet("top-booked-rooms")]
    [ProducesResponseType(
        typeof(List<TopBookedRoomResponse>),
        StatusCodes.Status200OK)]
    public ActionResult<List<TopBookedRoomResponse>>
        GetTop5MostFrequentlyBookedRooms()
    {
        logger.LogInformation("Получен запрос топ-5 бронируемых номеров");

        return Ok(analyticsService.GetTop5MostFrequentlyBookedRooms());
    }

    /// <summary>
    /// 4. Выводит количество бронирований для каждого номера
    /// </summary>
    [HttpGet("booking-count-by-room")]
    [ProducesResponseType(
        typeof(List<RoomBookingCountResponse>),
        StatusCodes.Status200OK)]
    public ActionResult<List<RoomBookingCountResponse>>
        GetBookingCountForEachRoom()
    {
        logger.LogInformation("Получен запрос количества бронирований по номерам");

        return Ok(analyticsService.GetBookingCountForEachRoom());
    }

    /// <summary>
    /// 5. Выводит топ-5 клиентов по суммарной стоимости проживания
    /// </summary>
    [HttpGet("top-clients-by-cost")]
    [ProducesResponseType(
        typeof(List<TopClientByCostResponse>),
        StatusCodes.Status200OK)]
    public ActionResult<List<TopClientByCostResponse>>
        GetTop5ClientsByTotalCost()
    {
        logger.LogInformation("Получен запрос топ-5 клиентов по стоимости проживания");

        return Ok(analyticsService.GetTop5ClientsByTotalCost());
    }
}