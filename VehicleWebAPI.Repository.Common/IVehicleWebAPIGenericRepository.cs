using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace VehicleWebAPI.Repository.Common
{
    public interface IVehicleWebAPIGenericRepository<T> where T : class
    {
        Task<int> SaveAsync();
        Task<EntityEntry<T>> CreateAsync(T entity);
        Task<T> ReadByIdAsync(int id);
        EntityEntry<T> Update(T entity);
        Task<EntityEntry<T>> DeleteAsync(int id);
        IQueryable<T> GetTable();
    }
}
