using Autofac;
using Microsoft.EntityFrameworkCore;
using VehicleWebAPI.DAL;
using VehicleWebAPI.Model;
using VehicleWebAPI.Repository;
using VehicleWebAPI.Repository.Common;
using VehicleWebAPI.Service;
using VehicleWebAPI.Service.Common;

namespace VehicleWebAPI.WebAPI
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<VehicleWebAPIDbContext>().As<DbContext>().InstancePerLifetimeScope();

            builder.RegisterType<VehicleWebAPIGenericRepository<VehicleMakeDataModel>>()
                                .As<IVehicleWebAPIGenericRepository<VehicleMakeDataModel>>()
                                .InstancePerLifetimeScope();

            builder.RegisterType<VehicleWebAPIGenericRepository<VehicleModelDataModel>>()
                                .As<IVehicleWebAPIGenericRepository<VehicleModelDataModel>>()
                                .InstancePerLifetimeScope();

            builder.RegisterType<VehicleMakeService>()
                                .As<IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel>>()
                                .InstancePerLifetimeScope();

            builder.RegisterType<VehicleModelService>()
                                .As<IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel>>()
                                .InstancePerLifetimeScope();
        }
    }
}