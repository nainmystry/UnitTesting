using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;

namespace RoomBookingApp.Infrastructure.Repositories;

public class RoomBookingService : IRoomBookingService
{
    private readonly RoomBookingAppDbContext _context;

    public RoomBookingService(RoomBookingAppDbContext context)
    {
        _context = context;       
    }
    public IEnumerable<Room> GetAvailableRooms(DateTime date)
    {
        //var bookedRooms = _context.RoomBookings.Where(r => r.Date == date).Select(q => q.RoomId).ToList();

        //var availbleRooms = _context.Rooms.Where(x => bookedRooms.Contains(x.Id) == false).ToList();

        var availbleRooms = _context.Rooms.Where(x => !x.RoomBookings.Any(p => p.Date == date)).ToList();
        
        return availbleRooms;
    }

    public void Save(RoomBooking roomBooking)
    {
        _context.Add(roomBooking);

        _context.SaveChanges();
    }
}
