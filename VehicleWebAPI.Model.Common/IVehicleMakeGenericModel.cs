using System.Collections.Generic;

namespace VehicleWebAPI.Model.Common
{
    public interface IVehicleMakeGenericModel<TEntity> where TEntity : class, IVehicleBaseModel
    {
        ICollection<TEntity> VehicleModels { get; set; }
    }
}
