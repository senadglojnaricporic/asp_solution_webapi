namespace VehicleWebAPI.WebAPI
{
    public class VehicleModelViewModel : IVehicleModelViewModel
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public VehicleMakeViewModel VehicleMake { get; set; }
    }
}