using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace VehicleWebAPI.Model
{
    public class VehicleMakeDataModel : IVehicleMakeDataModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public ICollection<VehicleModelDataModel> VehicleModels { get; set; }
    }
}
