using System;
using VehicleWebAPI.Repository.Common;
using VehicleWebAPI.Model;
using System.Threading.Tasks;
using VehicleWebAPI.Service.Common;
using System.Collections.Generic;
using VehicleWebAPI.Common;

namespace VehicleWebAPI.Service
{
    public class VehicleModelService : IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>
    {
        private readonly IVehicleWebAPIGenericRepository<VehicleModelDataModel> _vehicleModelRepository;

        public VehicleModelService(IVehicleWebAPIGenericRepository<VehicleModelDataModel> vehicleModelRepository)
        {
            _vehicleModelRepository = vehicleModelRepository;
        }

        public async Task<int> CreateItemAsync(IVehicleModelDataModel item)
        {
            await _vehicleModelRepository.CreateAsync((VehicleModelDataModel) item);
            return await _vehicleModelRepository.SaveAsync();
        }

        public async Task<IVehicleModelDataModel> ReadItemAsync(int id)
        {
            try
            {
                var vehicleModelDataModel = await _vehicleModelRepository.ReadByIdAsync(id);
                return vehicleModelDataModel;
            }
            catch(NullReferenceException)
            {
                return null;
            }
        }

        public async Task UpdateItemAsync(IVehicleModelDataModel item)
        {
            _vehicleModelRepository.Update((VehicleModelDataModel) item);
            await _vehicleModelRepository.SaveAsync();
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var res = await _vehicleModelRepository.DeleteAsync(id);
            if(res)
            {
                await _vehicleModelRepository.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<VehicleModelDataModel>> FindDataAsync(IFilteringGenericModel<VehicleModelDataModel> filtering, 
                                                                        ISortingGenericModel<VehicleModelDataModel> sorting, 
                                                                        IPagingGenericModel<VehicleModelDataModel> paging)
        {
            return await _vehicleModelRepository.FindDataAsync(filtering, sorting, paging);
        }
    }
}
