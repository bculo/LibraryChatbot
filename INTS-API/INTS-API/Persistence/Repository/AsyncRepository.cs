using INTS_API.Entities;
using INTS_API.Interfaces;
using INTS_DATASET.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace INTS_API.Persistence.Repository
{
    public class AsyncRepository<T> : IRepository<T> where T : class
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
                if(instance is Book)
                {
                    string projectPath = PathUtils.GetProjectDirectoryPath();
                    string txtPath = Path.Combine(projectPath, "Datasets", "error.txt");

                    Book temp = instance as Book;
                    File.AppendAllText(txtPath, temp.Title + Environment.NewLine);
                }

                return null;
            }
        }

        public async Task<bool> AddRangeAsync(List<T> instances)
        {
            try
            {
                _context.Set<T>().AddRange(instances);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException e)
            {
                return false;
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
