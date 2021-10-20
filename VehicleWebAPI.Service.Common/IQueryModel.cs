namespace VehicleWebAPI.Service.Common
{
    public interface IQueryModel
    {
        string sortBy { get; set; }
        bool sortAscending { get; set; }
        string filterType { get; set; }
        string searchString { get; set; }
        int lowerLimit { get; set; }
        int upperLimit { get; set; }
        int limit { get; set; }
        int pageIndex { get; set; }
        int pageSize { get; set; }
    }
}