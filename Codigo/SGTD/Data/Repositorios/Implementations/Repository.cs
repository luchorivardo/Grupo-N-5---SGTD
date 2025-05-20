using Data.Context;
using Data.Repositorios.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorios.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context { get; set; }

        public Repository(AppDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<T> FindAll()
        {
            return this._context.Set<T>().ToList();
        }
        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await this._context.Set<T>().ToListAsync();
        }
        public async Task<T> ObtenerPorId(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>().Where(expression).ToList();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this._context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
        public async Task SaveAsync()
        {
            try
            {
                await this._context.SaveChangesAsync();
                return;
            }
            catch
            {
                throw new Exception("Error saving changes.");
            }
        }
    }
}
