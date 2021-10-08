namespace VehicleWebAPI.Model.Common
{
    public interface IVehicleModelGenericModel<TEntity> where TEntity : class
    {
        int Id { get; set; }
        int MakeId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }

        TEntity VehicleMake { get; set; }
    }
}