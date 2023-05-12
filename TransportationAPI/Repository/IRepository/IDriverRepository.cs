using TransportationAPI.Models;

namespace TransportationAPI.Repository.IRepository
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver> UpdateDriverAsync(Driver driver);

    }
}
