using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomBookingController : ControllerBase
    {
        private readonly IRoomBookingRequestProcessor _roomBookingRequestProcessor;

        public RoomBookingController(IRoomBookingRequestProcessor roomBookingRequestProcessor)
        {
            _roomBookingRequestProcessor = roomBookingRequestProcessor;
        }

        public async Task<IActionResult> BookRoom(RoomBookingRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = _roomBookingRequestProcessor.BookRoom(request);
                if(result.Flag == BookingFlag.Success)
                {
                    return Ok(result);
                }

            }
            return BadRequest(ModelState);
        }

        public IActionResult Index()
        {
            return Ok();
        }
    }
}
