using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        private IRoomBookingService _roomBookingService;

        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }

        public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
        {
            if(bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }

            _roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingRequest));

            return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
        }

        /// <summary>
        /// Creates and returns a new instance of type TRoomBooking (which inherits from RoomBookingBase) by copying properties from the given RoomBookingRequest.
        /// </summary>
        /// <typeparam name="TRoomBooking"></typeparam>
        /// <param name="bookingRequest"></param>
        /// <returns></returns>
        private TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest) where TRoomBooking : RoomBookingBase, new()
        {
            return new TRoomBooking
            {
                Name = bookingRequest.Name,
                Email = bookingRequest.Email,
                ID = bookingRequest.ID,
                Date = bookingRequest.Date,
            };
        }
    }
}