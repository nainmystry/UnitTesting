namespace RoomBookingApp.Core.Models
{
    public class BookingRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}