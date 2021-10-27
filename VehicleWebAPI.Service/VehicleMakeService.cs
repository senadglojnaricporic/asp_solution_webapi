using System;
using VehicleWebAPI.Repository.Common;
using VehicleWebAPI.Model;
using AutoMapper;
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

        public async Task CreateItemAsync(IVehicleMakeDataModel item)
        {
            await _vehicleMakeRepository.CreateAsync((VehicleMakeDataModel) item);
            await _vehicleMakeRepository.SaveAsync();
        }

        public async Task<IVehicleMakeDataModel> ReadItemAsync(int id)
        {
            var vehicleMakeDataModel = await _vehicleMakeRepository.ReadByIdAsync(id);

            if(vehicleMakeDataModel == null)
            {
                return null;
            }

            return vehicleMakeDataModel;
        }

        public async Task UpdateItemAsync(IVehicleMakeDataModel item)
        {
            _vehicleMakeRepository.Update((VehicleMakeDataModel) item);
            await _vehicleMakeRepository.SaveAsync();
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var entry = await _vehicleMakeRepository.DeleteAsync(id);
            if(entry == null)
            {
                return false;
            }
            else
            {
                await _vehicleMakeRepository.SaveAsync();
                return true;
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
