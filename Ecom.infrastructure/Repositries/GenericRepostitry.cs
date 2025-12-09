using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecom.infrastructure.Repositries
{
    public class GenericRepostitry<T> : IGenericRepositry<T> where T : class
    {
        private readonly AppDContext _context;

        public GenericRepostitry(AppDContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() =>
            await _context.Set<T>().AsNoTracking().ToListAsync();


        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            
            var query = _context.Set<T>().AsQueryable();   
           
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {

        return await _context.Set<T>().FindAsync(id);

        }

        public Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id); 
        }

    

        public Task UpdateAsync(T entity)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
