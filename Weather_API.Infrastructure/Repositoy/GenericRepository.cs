using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_API.Infrastructure.Repositoy
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly Web_APIDbContext _context;
        private readonly DbSet<T> _db;
        public GenericRepository(Web_APIDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        /// <summary>
        /// Deletes an Object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            _db.Remove(await _db.FindAsync(id));
        }
        /// <summary>
        /// Deletes List of objects
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRangeAsync(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }
        /// <summary>
        /// Inserts An Object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(T entity)
        {
            try
            {
                await _db.AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(T item)
        {
            // attaches instance to the contex
            // t, then sets the state
            // as modified
            _db.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }


    }
}
