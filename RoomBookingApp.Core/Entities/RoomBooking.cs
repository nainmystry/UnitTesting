using RoomBookingApp.Core.Entities.BaseEntities;

namespace RoomBookingApp.Core.Domain;

public class RoomBooking : RoomBookingBase
{
    public int ID { get; set; }
    

    public Room Room { get; set; }
    public int RoomId { get; set; }

}