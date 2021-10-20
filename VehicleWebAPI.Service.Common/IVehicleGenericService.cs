using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace VehicleWebAPI.Service.Common
{
    public interface IVehicleGenericService<Entity> 
    where Entity : class 
    {
        Task CreateItemAsync(Entity item);
        Task<Entity> ReadItemAsync(int id);
        Task UpdateItemAsync(Entity item);
        Task<bool> DeleteItemAsync(int id);
    }
}
