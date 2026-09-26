using HotelSystem.Domain.Data;
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Shared.Enums;

namespace HotelSystem.Server.Services;

/// <summary>
/// Сервис для выполнения аналитических запросов по данным гостиницы
/// </summary>
public class AnalyticsService
{
    /// <summary>
    /// 1. Вывести информацию о всех клиентах,проживавших в номерах указанного типа, упорядочить по ФИО
    /// </summary>
    public List<Client> GetClientsByRoomCategory(RoomCategory roomCategory)
    {
        return HotelTestData.Bookings
            .Where(booking => booking.Room.RoomType.Category == roomCategory)
            .Select(booking => booking.Client)
            .DistinctBy(client => client.Id)
            .OrderBy(client => client.LastName)
            .ThenBy(client => client.FirstName)
            .ThenBy(client => client.Patronymic)
            .ToList();
    }

    /// <summary>
    /// 2. Вывести информацию о номерах, находящихся в текущем бронировании
    /// </summary>
    public List<Room> GetCurrentlyBookedRooms()
    {
        var currentDate = new DateTime(2026, 9, 10);

        return HotelTestData.Bookings
            .Where(booking =>
                booking.CheckInDate <= currentDate &&
                booking.CheckInDate.AddDays(booking.NumberOfDays) > currentDate)
            .Select(booking => booking.Room)
            .DistinctBy(room => room.Id)
            .OrderBy(room => room.RoomNumber)
            .ToList();
    }

    /// <summary>
    /// 3. Вывести топ 5 наиболее часто бронируемых номеров
    /// </summary>
    /// <summary>
    public object GetTop5MostFrequentlyBookedRooms()
    {
        return HotelTestData.Bookings
            .GroupBy(booking => booking.Room)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key.RoomNumber)
            .Take(5)
            .Select(group => new
            {
                RoomNumber = group.Key.RoomNumber,
                BookingCount = group.Count()
            })
            .ToList();
    }

    /// <summary>
    /// 4. Для каждого номера вывести число бронирований
    /// <summary>
    public Dictionary<int, int> GetBookingCountForEachRoom()
    {
        return HotelTestData.Bookings
            .GroupBy(booking => booking.Room.RoomNumber)
            .ToDictionary(
                group => group.Key,
                group => group.Count());
    }

    /// <summary>
    /// 5.Вывести топ 5 клиентов по суммарной стоимости проживания
    /// </summary>
    public object GetTop5ClientsByTotalCost()
    {
        return HotelTestData.Bookings
            .GroupBy(booking => booking.Client)
            .Select(group => new
            {
                Client = group.Key,
                TotalCost = group.Sum(booking => booking.TotalCost)
            })
            .OrderByDescending(x => x.TotalCost)
            .Take(5)
            .Select(x => new
            {
                LastName = x.Client.LastName,
                FirstName = x.Client.FirstName,
                Patronymic = x.Client.Patronymic,
                TotalCost = x.TotalCost
            })
            .ToList();
    }
}