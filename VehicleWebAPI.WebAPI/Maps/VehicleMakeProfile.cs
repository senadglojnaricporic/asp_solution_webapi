using AutoMapper;
using VehicleWebAPI.Model;
using VehicleWebAPI.Model.Common;

namespace VehicleWebAPI.WebAPI
{
    public class VehicleMakeProfile : Profile
    {
        public VehicleMakeProfile()
        {
            CreateMap<IVehicleMakeGenericModel<VehicleModelDataModel>, IVehicleMakeGenericModel<VehicleModelViewModel>>().ReverseMap();
            CreateMap<VehicleMakeDataModel, VehicleMakeViewModel>().ReverseMap();
        }

    }
}