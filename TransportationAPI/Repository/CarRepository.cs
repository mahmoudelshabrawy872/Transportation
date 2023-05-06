using TransportationAPI.Data;
using TransportationAPI.Models;
using TransportationAPI.Repository.IRepository;

namespace TransportationAPI.Repository
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly ApplicationDbContext _context;
        public CarRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return car;
        }
    }
}
