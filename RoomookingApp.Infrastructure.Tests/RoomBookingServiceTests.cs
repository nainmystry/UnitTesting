using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Infrastructure;
using RoomBookingApp.Infrastructure.Repositories;

namespace RoomookingApp.Infrastructure.Tests;

public class RoomBookingServiceTests
{
    [Fact]

    public void Should_Return_AvailableRooms()
    {
        //Arrange
        var date = new DateTime(2025, 06, 29);

        var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>()
                                .UseInMemoryDatabase("AvailableRoomTest")
                                .Options;

        using var context = new RoomBookingAppDbContext(dbOptions);        
        context.Add(new Room { Id = 1, Name = "Room 1" });
        context.Add(new Room { Id = 2, Name = "Room 2" });
        context.Add(new Room { Id = 3, Name = "Room 3" });

        context.Add(new RoomBooking { RoomId = 1, Date = date });
        context.Add(new RoomBooking { RoomId = 2, Date = date.AddDays(-1) });

        context.SaveChanges();
        
        var _roomBookingService = new RoomBookingService(context);


        //Act
        var availableRooms = _roomBookingService.GetAvailableRooms(date);

        availableRooms.Should().HaveCount(2);
    }
}