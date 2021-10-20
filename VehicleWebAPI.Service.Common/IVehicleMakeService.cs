using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleWebAPI.Model;

namespace VehicleWebAPI.Service.Common
{
    public interface IVehicleMakeService<T> : IVehicleGenericService<T> where T : class
    {
        Task<IEnumerable<VehicleMakeViewModel>> GetPageAsync(IFilteringGenericModel<VehicleMakeDataModel> filtering, 
                                                                        ISortingGenericModel<VehicleMakeDataModel> sorting, 
                                                                        IPagingGenericModel<VehicleMakeDataModel> paging);
    }
}