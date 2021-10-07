using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleWebAPI.Model.Common;

namespace VehicleWebAPI.Model
{
    public class VehicleModelDataModel : IVehicleModelGenericModel<VehicleMakeDataModel>
    {
        [Key]
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        [ForeignKey("MakeId")]
        public VehicleMakeDataModel VehicleMake { get; set; }
    }
}