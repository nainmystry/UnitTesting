using FluentAssertions;
using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Core;

public class RoomBookingRequestProcessorTests
{
    private RoomBookingRequestProcessor _processor;
    private RoomBookingRequest _bookingRequest;
    private Mock<IRoomBookingService> _roomBookingServiceMock;

    public RoomBookingRequestProcessorTests()
    {
        //Arrange 
        _bookingRequest = new RoomBookingRequest
        {
            ID = 1,
            Name = "Test",
            Email = "some@xyz.com",
            Date = DateTime.UtcNow
        };

        _roomBookingServiceMock = new Mock<IRoomBookingService>();
        _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
    }
    [Fact]
    public void Should_Return_RoomBooking_Request()
    {        
        //Act
        RoomBookingResult result = _processor.BookRoom(_bookingRequest);

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

    [Fact]
    public void Should_Save_RoomBooking_Request()
    {
        RoomBooking savedBooking = null;

        //It sets up the mock _roomBookingServiceMock to capture any RoomBooking passed to Save() using a callback,
        //and assigns it to savedBooking for later verification.
        _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
            .Callback<RoomBooking>(booking =>
            {
                savedBooking = booking;
            });

        _processor.BookRoom(_bookingRequest);

        //Expecting the below interface method, in this case "Save" to be called atleast once.
        _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Once);

        savedBooking.Should().NotBeNull();
        savedBooking.Name.Should().Be("Test");
        savedBooking.Email.Should().NotBeNullOrEmpty();
    }

}
