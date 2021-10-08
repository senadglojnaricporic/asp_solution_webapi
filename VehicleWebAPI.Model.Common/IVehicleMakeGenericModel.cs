using System.Collections.Generic;

namespace VehicleWebAPI.Model.Common
{
    public interface IVehicleMakeGenericModel<TEntity> where TEntity : class
    {
        int Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        ICollection<TEntity> VehicleModels { get; set; }
    }
}
