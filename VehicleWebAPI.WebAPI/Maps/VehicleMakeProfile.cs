using System.Collections.Generic;
using AutoMapper;
using VehicleWebAPI.Model;
using VehicleWebAPI.Service;
using VehicleWebAPI.Common;

namespace VehicleWebAPI.WebAPI
{
    public class VehicleMakeProfile : Profile
    {
        public VehicleMakeProfile()
        {
            CreateMap<IVehicleMakeDataModel, VehicleMakeDataModel>().ReverseMap();
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