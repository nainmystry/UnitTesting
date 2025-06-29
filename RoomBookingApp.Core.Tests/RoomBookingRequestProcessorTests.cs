using FluentAssertions;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Core;

public class RoomBookingRequestProcessorTests
{
    private RoomBookingRequestProcessor _processor;

    public RoomBookingRequestProcessorTests()
    {
        _processor = new RoomBookingRequestProcessor();
    }
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
        
        //Act
        RoomBookingResult result = _processor.BookRoom(bookingRequest);

        //Assert
        result.Should().NotBeNull();      
        result.Name.Should().Be("Test");
        result.Email.Should().Be("some@xyz.com");
    }

    [Fact]
    public void Should_Throw_NullException_For_NullRequest()
    {
        //Assert
        Action act = () => _processor.BookRoom(null);

        act.Should().Throw<ArgumentNullException>();
    }
}
