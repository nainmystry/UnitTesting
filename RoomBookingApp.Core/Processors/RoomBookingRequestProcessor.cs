using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Entities.BaseEntities;
using RoomBookingApp.Core.Enums;
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

            var avilableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date);
            var result = CreateRoomBookingObject<RoomBookingResult>(bookingRequest);

            if (avilableRooms != null && avilableRooms.Any())
            {
                _roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingRequest));
                result.ID = bookingRequest.ID;
                result.Flag = BookingFlag.Success;
                result.RoomBookingId = bookingRequest.ID;
            }
            else
            {
                result.Flag = BookingFlag.Failure;
            }

            return result;
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
                Date = bookingRequest.Date,
            };
        }
    }
}