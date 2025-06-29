using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomBookingApp.API.Controllers;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.API.Tests;

public class RoomBookingControllerTests
{
    private Mock<IRoomBookingRequestProcessor> _requestProcessorMock;
    private RoomBookingController _bookingController;
    private RoomBookingRequest _request;
    private RoomBookingResult _result;

    public RoomBookingControllerTests()
    {
        _requestProcessorMock = new Mock<IRoomBookingRequestProcessor>();
        _bookingController = new RoomBookingController(_requestProcessorMock.Object);
        _result = new RoomBookingResult();
        _request = new RoomBookingRequest();

        _requestProcessorMock.Setup(x => x.BookRoom(_request)).Returns(_result);
    }

    [Theory]
    [InlineData(1, true, typeof(OkObjectResult))]
    [InlineData(0, false, typeof(BadRequestObjectResult))]
    public async Task Should_Call_Booking_Method_When_Valid(int expectedMethodCalls, bool isModelValid, 
        Type expectedActionResult)
    {
        //Arrange

        if(!isModelValid)
        {
            _bookingController.ModelState.AddModelError("Key", "ErrorMessage");
        }

        //Act
        var result = await _bookingController.BookRoom(_request);


        //Assert
        result.Should().BeOfType(expectedActionResult);
        _requestProcessorMock.Verify(x => x.BookRoom(_request), Times.Exactly(expectedMethodCalls));
    }
    

}
