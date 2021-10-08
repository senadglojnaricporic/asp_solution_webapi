using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleWebAPI.Common;
using VehicleWebAPI.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace VehicleWebAPI.Repository
{
    public class VehicleWebAPIGenericRepository : IVehicleWebAPIGenericRepository
    {
        private readonly DbContext _DbContext;

        public VehicleWebAPIGenericRepository(DbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task SaveAsync<T>() where T : class
        {
            await _DbContext.SaveChangesAsync();
        }

        public async Task CreateAsync<T>(T entity) where T : class
        {
            await _DbContext.Set<T>().AddAsync(entity);
        }

        public async Task<T> ReadByIdAsync<T>(int id) where T : class
        {
            return await _DbContext.Set<T>().FindAsync(id);
        }

        public void UpdateAsync<T>(T entity) where T : class
        {
            _DbContext.Set<T>().Update(entity);
        }

        public async Task DeleteAsync<T>(int id) where T : class
        {
            var entity = await _DbContext.Set<T>().FindAsync(id);
            _DbContext.Set<T>().Remove(entity);
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
