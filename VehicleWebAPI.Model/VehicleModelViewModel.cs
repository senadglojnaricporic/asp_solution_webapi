using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleWebAPI.Model.Common;

namespace VehicleWebAPI.Model
{
    public class VehicleModelViewModel : IVehicleModelGenericModel<VehicleMakeViewModel>
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public VehicleMakeViewModel VehicleMake { get; set; }
    }
}