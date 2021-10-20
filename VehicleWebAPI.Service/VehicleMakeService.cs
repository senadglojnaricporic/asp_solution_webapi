using System;
using VehicleWebAPI.Repository.Common;
using VehicleWebAPI.Model;
using VehicleWebAPI.Model.Common;
using AutoMapper;
using System.Threading.Tasks;
using VehicleWebAPI.Service.Common;
using System.Collections.Generic;

namespace VehicleWebAPI.Service
{
    public class VehicleMakeService : IVehicleMakeService<IVehicleMakeGenericModel<VehicleModelViewModel>>
    {
        private readonly IVehicleWebAPIGenericRepository<VehicleMakeDataModel> _vehicleMakeRepository;
        private readonly IMapper _mapper;

        public VehicleMakeService(IVehicleWebAPIGenericRepository<VehicleMakeDataModel> vehicleMakeRepository,
                                IMapper mapper)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
            _mapper = mapper;
        }

        public async Task CreateItemAsync(IVehicleMakeGenericModel<VehicleModelViewModel> item)
        {
            var vehicleMakeDataModel = _mapper.Map<VehicleMakeDataModel>(item);
            await _vehicleMakeRepository.CreateAsync(vehicleMakeDataModel);
            await _vehicleMakeRepository.SaveAsync();
        }

        public async Task<IVehicleMakeGenericModel<VehicleModelViewModel>> ReadItemAsync(int id)
        {
            var vehicleMakeDataModel = await _vehicleMakeRepository.ReadByIdAsync(id);

            if(vehicleMakeDataModel == null)
            {
                return null;
            }

            var vehicleMakeViewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMakeDataModel);
            return vehicleMakeViewModel;
        }

        public async Task UpdateItemAsync(IVehicleMakeGenericModel<VehicleModelViewModel> item)
        {
            var vehicleMakeDataModel = _mapper.Map<VehicleMakeDataModel>(item);
            _vehicleMakeRepository.Update(vehicleMakeDataModel);
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

        public async Task<IEnumerable<VehicleMakeViewModel>> GetPageAsync(IFilteringGenericModel<VehicleMakeDataModel> filtering, 
                                                                        ISortingGenericModel<VehicleMakeDataModel> sorting, 
                                                                        IPagingGenericModel<VehicleMakeDataModel> paging)
        {
            var source = _vehicleMakeRepository.GetTable();

            if(!String.IsNullOrEmpty(filtering.filterType))
            {
                filtering.Filter(ref source);
            }

            sorting.Sort(ref source);

            var listDataModel = await paging.PageAsync(source);
            var listViewmodel = _mapper.Map<List<VehicleMakeViewModel>>(listDataModel);

            return listViewmodel;
        }
    }
}
