namespace VehicleWebAPI.Model.Common
{
    public interface IVehicleBaseModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }

    }
}