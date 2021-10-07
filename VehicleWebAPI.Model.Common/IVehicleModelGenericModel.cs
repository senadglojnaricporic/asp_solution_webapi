namespace VehicleWebAPI.Model.Common
{
    public interface IVehicleModelGenericModel<T> where TEntity : class, IVehicleBaseModel
    {
        int MakeId { get; set; }

        T VehicleMake { get; set; }
    }
}