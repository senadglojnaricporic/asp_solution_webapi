using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VehicleWebAPI.Model.Common;

namespace VehicleWebAPI.Model
{
    public class VehicleMakeDataModel : IVehicleMakeGenericModel<VehicleModelDataModel>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public ICollection<VehicleModelDataModel> VehicleModels { get; set; }
    }
}
