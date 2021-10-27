namespace VehicleWebAPI.WebAPI
{
    public interface IVehicleModelViewModel
    {
        int Id { get; set; }
        int MakeId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        VehicleMakeViewModel VehicleMake { get; set; }
    }
}