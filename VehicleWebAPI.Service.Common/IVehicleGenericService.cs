using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleWebAPI.Common;

namespace VehicleWebAPI.Service.Common
{
    public interface IVehicleGenericService<Entity, T> 
    where Entity : class 
    where T : class
    {
        Task CreateItemAsync(Entity item);
        Task<Entity> ReadItemAsync(int id);
        Task UpdateItemAsync(Entity item);
        Task<bool> DeleteItemAsync(int id);
        Task<IEnumerable<T>> FindDataAsync(IFilteringGenericModel<T> filtering, 
                                                                        ISortingGenericModel<T> sorting, 
                                                                        IPagingGenericModel<T> paging);
    }
}
