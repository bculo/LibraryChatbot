using INTS_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INTS_API.Persistence.Repository
{
    public abstract class AsyncRepository<T> : IRepository<T> where T : class
    {
        protected readonly LibraryDBContext _context;

        public AsyncRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public virtual async Task<T> AddAsync(T instance)
        {
            try
            {
                _context.Set<T>().Add(instance);
                await _context.SaveChangesAsync();
                return instance;
            }
            catch (DbUpdateException e)
            {
                return null;
            }
        }

        public virtual async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public virtual async Task<bool> DeleteAsync(T instance)
        {
            try
            {
                _context.Set<T>().Remove(instance);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(object instanceId)
        {
            try
            {
                return await _context.Set<T>().FindAsync(instanceId);
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task<T> UpdateAsync(T instance)
        {
            try
            {
                _context.Entry(instance).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return instance;

            }
            catch (DbUpdateException e)
            {
                return null;
            }
        }
    }
}
