using System.Threading.Tasks;
using VehicleWebAPI.Model.Common;

namespace VehicleWebAPI.Repository.Common
{
    public class IVehicleWebAPIGenericRepository
    {
        Task CreateAsync<T>(T entity) where T : IVehicleBaseModel;
        Task<T> ReadByIdAsync<T>(int id) where T : IVehicleBaseModel;
        Task UpdateAsync<T>(T entity) where T : IVehicleBaseModel;
        Task DeleteAsync<T>(int id) where T : IVehicleBaseModel;
        
    }
}
