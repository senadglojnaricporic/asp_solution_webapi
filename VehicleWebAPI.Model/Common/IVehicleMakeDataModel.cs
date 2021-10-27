using System.Collections.Generic;

namespace VehicleWebAPI.Model
{
    public interface IVehicleMakeDataModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        ICollection<VehicleModelDataModel> VehicleModels { get; set; }
    }
}
