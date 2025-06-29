using RoomBookingApp.Core.Entities.BaseEntities;
using RoomBookingApp.Core.Enums;

namespace RoomBookingApp.Core.Models
{
    public class RoomBookingResult : RoomBookingBase
    {
        public int ID { get; set; }

        public BookingFlag Flag { get; set; }
        public int? RoomBookingId { get; set; }
    }
}