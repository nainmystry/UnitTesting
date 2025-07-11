﻿using FluentAssertions;
using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Core;

public class RoomBookingRequestProcessorTests
{
    private RoomBookingRequestProcessor _processor;
    private RoomBookingRequest _bookingRequest;
    private Mock<IRoomBookingService> _roomBookingServiceMock;
    private List<Room> _availableRooms;

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

        _availableRooms = new List<Room>() { new Room() { Id = 1 } };
        _roomBookingServiceMock = new Mock<IRoomBookingService>();
        _roomBookingServiceMock.Setup(q => q.GetAvailableRooms(_bookingRequest.Date))
            .Returns(_availableRooms);
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
        savedBooking.ID.Should().Be(_availableRooms.First().Id);
    }

    [Fact]
    public void Should_NotBook_If_NoRoomAvailable()
    {
       _availableRooms.Clear();

       _processor.BookRoom(_bookingRequest);

       _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Never);   

    }

    [Theory]
    [InlineData(BookingFlag.Success, true)]
    [InlineData(BookingFlag.Failure, false)]
    public void Should_Return_SuccessOrFailure_Flag_InResult(BookingFlag successFlag, bool isAvailable)
    {
        if(!isAvailable)
        {
            _availableRooms.Clear();
        }
        var result = _processor.BookRoom(_bookingRequest);
        successFlag.Should().Be(result.Flag);
    }


    [Theory]
    [InlineData(1, true)]
    [InlineData(null, false)]
    public void Should_Return_RoomBookingId_In_Result(int? roomBookingId, bool isAvailable)
    {
        if (!isAvailable)
        {
            _availableRooms.Clear();
        }
        else
        {
            _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
            .Callback<RoomBooking>(booking =>
            {
                booking.RoomId = roomBookingId.Value;
            });
        }

        var result = _processor.BookRoom(_bookingRequest);
        result.RoomBookingId.Should().Be(roomBookingId);
    }
}
