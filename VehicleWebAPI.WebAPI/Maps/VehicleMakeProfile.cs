using System.Collections.Generic;
using AutoMapper;
using VehicleWebAPI.Model;
using VehicleWebAPI.Model.Common;
using VehicleWebAPI.Service;
using VehicleWebAPI.Service.Common;

namespace VehicleWebAPI.WebAPI
{
    public class VehicleMakeProfile : Profile
    {
        public VehicleMakeProfile()
        {
            CreateMap<IVehicleMakeGenericModel<VehicleModelDataModel>, IVehicleMakeGenericModel<VehicleModelViewModel>>().ReverseMap();
            CreateMap<VehicleMakeDataModel, VehicleMakeViewModel>().ReverseMap();
            CreateMap<List<VehicleMakeDataModel>, List<VehicleMakeViewModel>>().ReverseMap();
            CreateMap<IQueryModel, IFilteringGenericModel<VehicleMakeDataModel>>();
            CreateMap<IQueryModel, ISortingGenericModel<VehicleMakeDataModel>>();
            CreateMap<IQueryModel, IPagingGenericModel<VehicleMakeDataModel>>();
            CreateMap<QueryModel, FilteringMakeModel>();
            CreateMap<QueryModel, SortingMakeModel>();
            CreateMap<QueryModel, PagingMakeModel>();
        }

    }
}