using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        public RoomBookingRequestProcessor()
        {
        }

        public RoomBookingResult BookRoom(BookingRequest bookingRequest)
        {
            if(bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }

            return new RoomBookingResult
            {
                Name = bookingRequest.Name,
                Email = bookingRequest.Email,
                Date = bookingRequest.Date,
                ID = bookingRequest.ID
            };
        }
    }
}