using System.Collections.Generic;

namespace VehicleWebAPI.WebAPI
{
    public interface IVehicleMakeViewModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        ICollection<VehicleModelViewModel> VehicleModels { get; set; }
    }
}