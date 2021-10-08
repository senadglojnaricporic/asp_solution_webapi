using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using VehicleWebAPI.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace VehicleWebAPI.Repository.Common
{
    public interface IVehicleWebAPIGenericRepository
    {
        Task<int> SaveAsync<T>() where T : class;
        Task<EntityEntry<T>> CreateAsync<T>(T entity) where T : class;
        Task<T> ReadByIdAsync<T>(int id) where T : class;
        EntityEntry<T> Update<T>(T entity) where T : class;
        Task<EntityEntry<T>> DeleteAsync<T>(int id) where T : class;
        Task<IList<T>> GetListAsync<T>(ListParams parameters, Func<IQueryable<T>, string, string, IQueryable<T>> filterAndSort = null) where T : class;
    }
}
