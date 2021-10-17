using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VehicleWebAPI.Model.Common;

namespace VehicleWebAPI.Model
{
    public class VehicleMakeViewModel : IVehicleMakeGenericModel<VehicleModelViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public ICollection<VehicleModelViewModel> VehicleModels { get; set; }
    }
}