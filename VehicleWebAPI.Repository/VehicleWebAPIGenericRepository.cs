using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleWebAPI.Common;
using VehicleWebAPI.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace VehicleWebAPI.Repository
{
    public class VehicleWebAPIGenericRepository : IVehicleWebAPIGenericRepository
    {
        private readonly DbContext _DbContext;

        public VehicleWebAPIGenericRepository(DbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<int> SaveAsync<T>() where T : class
        {
            return await _DbContext.SaveChangesAsync();
        }

        public async Task<EntityEntry<T>> CreateAsync<T>(T entity) where T : class
        {
            return await _DbContext.Set<T>().AddAsync(entity);
        }

        public async Task<T> ReadByIdAsync<T>(int id) where T : class
        {
            return await _DbContext.Set<T>().FindAsync(id);
        }

        public EntityEntry<T> Update<T>(T entity) where T : class
        {
            return _DbContext.Set<T>().Update(entity);
        }

        public async Task<EntityEntry<T>> DeleteAsync<T>(int id) where T : class
        {
            var entity = await _DbContext.Set<T>().FindAsync(id);
            return _DbContext.Set<T>().Remove(entity);
        }

        public async Task<IList<T>> GetListAsync<T>(ListParams parameters, Func<IQueryable<T>, string, string, IQueryable<T>> filterAndSort = null) where T : class
        {
            var _source = from x in _DbContext.Set<T>() select x;

            if(filterAndSort != null)
            {
                _source = filterAndSort(_source, parameters.searchString, parameters.sortOrder);
            }

            return await _source.Skip(parameters.listStart).Take(parameters.listSize).ToListAsync();

        }
    }
}
