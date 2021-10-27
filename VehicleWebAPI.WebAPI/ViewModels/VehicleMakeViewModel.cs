using System.Collections.Generic;

namespace VehicleWebAPI.WebAPI
{
    public class VehicleMakeViewModel : IVehicleMakeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public ICollection<VehicleModelViewModel> VehicleModels { get; set; }
    }
}