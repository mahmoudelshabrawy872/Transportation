using TransportationAPI.Data;
using TransportationAPI.Models;
using TransportationAPI.Repository.IRepository;

namespace TransportationAPI.Repository
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        private readonly ApplicationDbContext _context;
        public DriverRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Driver> UpdateDriverAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
            return driver;
        }
    }
}
