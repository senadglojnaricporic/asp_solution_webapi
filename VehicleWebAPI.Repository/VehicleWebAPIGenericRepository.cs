using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleWebAPI.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VehicleWebAPI.Common;

namespace VehicleWebAPI.Repository
{
    public class VehicleWebAPIGenericRepository<T> : IVehicleWebAPIGenericRepository<T> 
    where T : class
    
    {
        private readonly DbContext _DbContext;

        public VehicleWebAPIGenericRepository(DbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<int> SaveAsync()
        {
            return await _DbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(T entity) 
        {
            await _DbContext.Set<T>().AddAsync(entity);
        }

        public async Task<T> ReadByIdAsync(int id) 
        {
            return await _DbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity) 
        {
            _DbContext.Set<T>().Update(entity);
        }

        public async Task<bool> DeleteAsync(int id) 
        {
            try
            {
                var entity = await _DbContext.Set<T>().FindAsync(id);
                _DbContext.Set<T>().Remove(entity);
                return true;
            }
            catch(ArgumentNullException)
            {
                return false;
            }
        }

        public IQueryable<T> GetTable()
        {
             var _source = from x in _DbContext.Set<T>() select x;
             return _source;
        }

        public async Task<IEnumerable<T>> FindDataAsync(IFilteringGenericModel<T> filtering, ISortingGenericModel<T> sorting, IPagingGenericModel<T> paging)
        {
            var source = GetTable();

            if(!String.IsNullOrEmpty(filtering.filterType))
            {
                filtering.Filter(ref source);
            }

            sorting.Sort(ref source);

            var listDataModel = await paging.PageAsync(source);

            return listDataModel;
        }
    }
}
