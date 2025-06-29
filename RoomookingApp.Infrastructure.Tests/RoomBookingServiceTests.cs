using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Infrastructure;
using RoomBookingApp.Infrastructure.Repositories;

namespace RoomookingApp.Infrastructure.Tests;

public class RoomBookingServiceTests
{
    public static DbContextOptions<RoomBookingAppDbContext> dbOptions;

    public RoomBookingServiceTests()
    {
       dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>()
                        .UseInMemoryDatabase("AvailableRoomTest")
                        .Options;
    }



    [Fact]
    public void Should_Return_AvailableRooms()
    {
        //Arrange
        var date = new DateTime(2025, 06, 29);

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


    [Fact]
    public void Should_Save_RoomBooking()
    {
        //Arrange
        var roomBooking = new RoomBooking() { RoomId = 1, Date = new DateTime(2025, 06, 29) };


        //Act
        using var context = new RoomBookingAppDbContext(dbOptions);
        var roomBookingService = new RoomBookingService(context);
        roomBookingService.Save(roomBooking);

        var bookings = context.RoomBookings.ToList();

        var booking = Assert.Single(bookings);
        var booked = bookings.Should().ContainSingle().Subject;

        Assert.Equal(roomBooking.Date, booking.Date);
        Assert.Equal(roomBooking.RoomId, booking.RoomId);
        
    }
}