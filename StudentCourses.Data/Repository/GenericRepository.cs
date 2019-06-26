using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourses.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private SchoolDBContext _context = null;
        private DbSet<T> table = null;

        
        public GenericRepository(SchoolDBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await table.FindAsync(id);
        }
        public async Task Insert(T obj)
        {
             table.Add(obj);
             await _context.SaveChangesAsync();
        }
        public async Task Update(T obj)
        {
             table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            await _context.SaveChangesAsync();
        }
       
    }
}
