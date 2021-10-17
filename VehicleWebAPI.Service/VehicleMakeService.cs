using System;
using VehicleWebAPI.Repository.Common;
using VehicleWebAPI.Model;
using VehicleWebAPI.Model.Common;
using AutoMapper;
using System.Threading.Tasks;

namespace VehicleWebAPI.Service
{
    public class VehicleMakeService
    {
        private readonly IVehicleWebAPIGenericRepository<VehicleMakeDataModel> _vehicleMakeRepository;
        private readonly IMapper _mapper;

        public VehicleMakeService(IVehicleWebAPIGenericRepository<VehicleMakeDataModel> vehicleMakeRepository,
                                IMapper mapper)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
            _mapper = mapper;
        }

        public async Task CreateItem(IVehicleMakeGenericModel<VehicleModelViewModel> item)
        {
            var vehicleMakeDataModel = _mapper.Map<VehicleMakeDataModel>(item);
            await _vehicleMakeRepository.CreateAsync(vehicleMakeDataModel);
            await _vehicleMakeRepository.SaveAsync();
        }

        public async Task<IVehicleMakeGenericModel<VehicleModelViewModel>> ReadItem(int id)
        {
            var vehicleMakeDataModel = await _vehicleMakeRepository.ReadByIdAsync(id);
            var vehicleMakeViewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMakeDataModel);
            return vehicleMakeViewModel;
        }

        public async Task UpdateItem(IVehicleMakeGenericModel<VehicleModelViewModel> item)
        {
            var vehicleMakeDataModel = _mapper.Map<VehicleMakeDataModel>(item);
            _vehicleMakeRepository.Update(vehicleMakeDataModel);
            await _vehicleMakeRepository.SaveAsync();
        }

        public async Task DeleteItem(int id)
        {
            await _vehicleMakeRepository.DeleteAsync(id);
            await _vehicleMakeRepository.SaveAsync();
        }

        
    }
}
