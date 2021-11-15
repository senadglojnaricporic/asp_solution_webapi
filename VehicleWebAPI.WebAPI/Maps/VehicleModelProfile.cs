using System.Collections.Generic;
using AutoMapper;
using VehicleWebAPI.Model;
using VehicleWebAPI.Service;
using VehicleWebAPI.Common;

namespace VehicleWebAPI.WebAPI
{
    public class VehicleModelProfile : Profile
    {
        public VehicleModelProfile()
        {
            CreateMap<IVehicleModelDataModel, VehicleModelDataModel>().ReverseMap();
            CreateMap<VehicleModelDataModel, VehicleModelViewModel>().ReverseMap();
            CreateMap<List<VehicleModelDataModel>, List<VehicleModelViewModel>>().ReverseMap();
            CreateMap<IQueryModel, IFilteringGenericModel<VehicleModelDataModel>>();
            CreateMap<IQueryModel, ISortingGenericModel<VehicleModelDataModel>>();
            CreateMap<IQueryModel, IPagingGenericModel<VehicleModelDataModel>>();
            CreateMap<QueryModel, FilteringModelModel>();
            CreateMap<QueryModel, SortingModelModel>();
            CreateMap<QueryModel, PagingModelModel>();
        }
    }
}