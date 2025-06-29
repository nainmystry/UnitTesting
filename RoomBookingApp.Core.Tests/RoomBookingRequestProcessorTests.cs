using FluentAssertions;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Core;

public class RoomBookingRequestProcessorTests
{
    [Fact]
    public void Should_Return_RoomBooking_Request()
    {
        //Arrange 
        var bookingRequest = new BookingRequest
        {
            ID = 1,
            Name = "Test",
            Email = "some@xyz.com",
            Date = DateTime.UtcNow
        };

        var processor = new RoomBookingRequestProcessor();

        //Act
        RoomBookingResult result = processor.BookRoom(bookingRequest);

        //Assert
        result.Should().NotBeNull();      
        result.Name.Should().Be("Test");
        result.Email.Should().Be("some@xyz.com");
    }
}
