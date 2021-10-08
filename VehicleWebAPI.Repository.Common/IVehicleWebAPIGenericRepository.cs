using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using VehicleWebAPI.Common;
using System.Linq;

namespace VehicleWebAPI.Repository.Common
{
    public interface IVehicleWebAPIGenericRepository
    {
        Task SaveAsync<T>() where T : class;
        Task CreateAsync<T>(T entity) where T : class;
        Task<T> ReadByIdAsync<T>(int id) where T : class;
        void UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(int id) where T : class;
        Task<IList<T>> GetListAsync<T>(ListParams parameters, Func<IQueryable<T>, string, string, IQueryable<T>> filterAndSort = null) where T : class;
    }
}
