using HotelSystem.Domain.Enums;
using HotelSystem.Tests.TestData;

namespace HotelSystem.Tests;

public class HotelTests
{
    [Fact]
    public void GetClientsByRoomType_ReturnsClientsOrderedByFullName()
    {
        var roomCategory = RoomCategory.Standard;

        var result = HotelTestData.Bookings
            .Where(booking => booking.Room.RoomType.Category == roomCategory)
            .Select(booking => booking.Client)
            .DistinctBy(client => client.PassportNumber)
            .OrderBy(client => client.FullName)
            .ToList();

        Assert.Equal(
            new[]
            {
                "Белов Борис Сергеевич",
                "Васильева Виктория Андреевна",
                "Громов Георгий Иванович",
                "Егоров Евгений Максимович",
                "Жукова Жанна Романовна"
            },
            result.Select(client => client.FullName).ToArray());
    }

    [Fact]
    public void GetCurrentlyBookedRooms_ReturnsActiveBookings()
    {
        var currentDate = new DateTime(2026, 9, 10);

        var result = HotelTestData.Bookings
            .Where(booking =>
                booking.CheckInDate <= currentDate &&
                booking.CheckInDate.AddDays(booking.NumberOfDays) > currentDate)
            .Select(booking => booking.Room)
            .DistinctBy(room => room.RoomNumber)
            .OrderBy(room => room.RoomNumber)
            .ToList();

        Assert.Equal(
            new[]
            {
                103,
                203,
                401
            },
            result.Select(room => room.RoomNumber).ToArray());
    }

    [Fact]
    public void GetTop5MostFrequentlyBookedRooms_ReturnsCorrectRooms()
    {
        var result = HotelTestData.Bookings
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

        Assert.Equal(
            new[]
            {
                203,
                102,
                202,
                101,
                103
            },
            result.Select(room => room.RoomNumber).ToArray());

        Assert.Equal(
            new[]
            {
                3,
                2,
                2,
                1,
                1
            },
            result.Select(room => room.BookingCount).ToArray());
    }

    [Fact]
    public void GetBookingCountForEachRoom_ReturnsCorrectCounts()
    {
        var result = HotelTestData.Rooms
            .Select(room => new
            {
                RoomNumber = room.RoomNumber,
                BookingCount = HotelTestData.Bookings.Count(
                    booking => booking.Room.RoomNumber == room.RoomNumber)
            })
            .OrderBy(room => room.RoomNumber)
            .ToList();

        Assert.Equal(
            new[]
            {
                101,
                102,
                103,
                201,
                202,
                203,
                301,
                302,
                303,
                401
            },
            result.Select(room => room.RoomNumber).ToArray());

        Assert.Equal(
            new[]
            {
                1,
                2,
                1,
                1,
                2,
                3,
                1,
                1,
                1,
                1
            },
            result.Select(room => room.BookingCount).ToArray());
    }

    [Fact]
    public void GetTop5ClientsByTotalAccommodationCost_ReturnsCorrectClients()
    {
        var result = HotelTestData.Bookings
            .GroupBy(booking => booking.Client)
            .Select(group => new
            {
                Client = group.Key,
                TotalCost = group.Sum(booking =>
                    booking.Room.RoomType.PricePerDay * booking.NumberOfDays)
            })
            .OrderByDescending(x => x.TotalCost)
            .Take(5)
            .ToList();

        Assert.Equal(
            new[]
            {
                "Васильева Виктория Андреевна",
                "Захаров Захар Денисович",
                "Громов Георгий Иванович",
                "Иванова Ирина Павловна",
                "Белов Борис Сергеевич"
            },
            result.Select(x => x.Client.FullName).ToArray());

        Assert.Equal(
            new[]
            {
                106000m,
                95000m,
                89000m,
                47500m,
                32700m
            },
            result.Select(x => x.TotalCost).ToArray());
    }
}
