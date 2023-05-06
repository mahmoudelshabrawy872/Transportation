using TransportationAPI.Models;

namespace TransportationAPI.Repository.IRepository
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<Car> UpdateCarAsync(Car car);
    }
}
