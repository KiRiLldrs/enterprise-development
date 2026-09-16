using Domain.Shared.Enums;
using HotelSystem.Domain.Data;

namespace HotelSystem.Tests;

public class HotelTests
{
    [Fact]
    public void GetClientsByRoomType()
    {
        var roomCategory = RoomCategory.Standard;

        var expectedClients = new[]
        {
            "Белов Борис Сергеевич",
            "Васильева Виктория Андреевна",
            "Громов Георгий Иванович",
            "Егоров Евгений Максимович",
            "Жукова Жанна Романовна"
        };

        var result = HotelTestData.Bookings
            .Where(booking => booking.Room.RoomType.Category == roomCategory)
            .Select(booking => booking.Client)
            .DistinctBy(client => client.PassportNumber)
            .OrderBy(client => client.FullName)
            .ToList();

        Assert.Equal(
            expectedClients,
            result.Select(client => client.FullName).ToArray());
    }

    [Fact]
    public void GetCurrentlyBookedRooms()
    {
        var currentDate = new DateTime(2026, 9, 10);

        var expectedRooms = new[]
        {
            103,
            203,
            401
        };

        var result = HotelTestData.Bookings
            .Where(booking =>
                booking.CheckInDate <= currentDate &&
                booking.CheckInDate.AddDays(booking.NumberOfDays) > currentDate)
            .Select(booking => booking.Room)
            .DistinctBy(room => room.RoomNumber)
            .OrderBy(room => room.RoomNumber)
            .ToList();

        Assert.Equal(
            expectedRooms,
            result.Select(room => room.RoomNumber).ToArray());
    }

    [Fact]
    public void GetTop5MostFrequentlyBookedRooms()
    {
        var expectedRoomNumbers = new[]
        {
            203,
            102,
            202,
            101,
            103
        };

        var expectedBookingCounts = new[]
        {
            3,
            2,
            2,
            1,
            1
        };

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
            expectedRoomNumbers,
            result.Select(room => room.RoomNumber).ToArray());

        Assert.Equal(
            expectedBookingCounts,
            result.Select(room => room.BookingCount).ToArray());
    }

    [Fact]
    public void GetBookingCountForEachRoom()
    {
        var expectedRoomNumbers = new[]
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
        };

        var expectedBookingCounts = new[]
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
        };

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
            expectedRoomNumbers,
            result.Select(room => room.RoomNumber).ToArray());

        Assert.Equal(
            expectedBookingCounts,
            result.Select(room => room.BookingCount).ToArray());
    }

    [Fact]
    public void GetTop5ClientsByTotalCost()
    {
        var expectedClients = new[]
        {
            "Васильева Виктория Андреевна",
            "Захаров Захар Денисович",
            "Громов Георгий Иванович",
            "Иванова Ирина Павловна",
            "Белов Борис Сергеевич"
        };

        var expectedCosts = new[]
        {
            106000m,
            95000m,
            89000m,
            47500m,
            32700m
        };

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
            expectedClients,
            result.Select(x => x.Client.FullName).ToArray());

        Assert.Equal(
            expectedCosts,
            result.Select(x => x.TotalCost).ToArray());
    }
}
