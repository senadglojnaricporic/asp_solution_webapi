using System;
using VehicleWebAPI.Repository.Common;
using VehicleWebAPI.Model;
using System.Threading.Tasks;
using VehicleWebAPI.Service.Common;
using System.Collections.Generic;
using VehicleWebAPI.Common;

namespace VehicleWebAPI.Service
{
    public class VehicleMakeService : IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>
    {
        private readonly IVehicleWebAPIGenericRepository<VehicleMakeDataModel> _vehicleMakeRepository;

        public VehicleMakeService(IVehicleWebAPIGenericRepository<VehicleMakeDataModel> vehicleMakeRepository)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
        }

        public async Task<int> CreateItemAsync(IVehicleMakeDataModel item)
        {
            await _vehicleMakeRepository.CreateAsync((VehicleMakeDataModel) item);
            return await _vehicleMakeRepository.SaveAsync();
        }

        public async Task<IVehicleMakeDataModel> ReadItemAsync(int id)
        {
            try
            {
                var vehicleMakeDataModel = await _vehicleMakeRepository.ReadByIdAsync(id);
                return vehicleMakeDataModel;
            }
            catch(NullReferenceException)
            {
                return null;
            }
        }

        public async Task UpdateItemAsync(IVehicleMakeDataModel item)
        {
            _vehicleMakeRepository.Update((VehicleMakeDataModel) item);
            await _vehicleMakeRepository.SaveAsync();
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var res = await _vehicleMakeRepository.DeleteAsync(id);
            if(res)
            {
                await _vehicleMakeRepository.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<VehicleMakeDataModel>> FindDataAsync(IFilteringGenericModel<VehicleMakeDataModel> filtering, 
                                                                        ISortingGenericModel<VehicleMakeDataModel> sorting, 
                                                                        IPagingGenericModel<VehicleMakeDataModel> paging)
        {
            return await _vehicleMakeRepository.FindDataAsync(filtering, sorting, paging);
        }
    }
}
