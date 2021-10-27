namespace VehicleWebAPI.Model
{
    public interface IVehicleModelDataModel
    {
        int Id { get; set; }
        int MakeId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }

        VehicleMakeDataModel VehicleMake { get; set; }
    }
}