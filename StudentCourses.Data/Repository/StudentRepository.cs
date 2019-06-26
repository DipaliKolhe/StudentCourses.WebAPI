using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourses.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private SchoolDBContext _context = null;
        private DbSet<Student> _students = null;


        public StudentRepository(SchoolDBContext _context)
        {
            this._context = _context;
            _students = _context.Set<Student>();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _students.ToListAsync();
        }
        public async Task<Student> GetById(int id)
        {
            return await _students.FindAsync(id);
        }

        public async Task Insert(Student obj)
        {
            _students.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Student obj)
        {
            _students.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            Student existing = _students.Find(id);
            _students.Remove(existing);
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetCoursesById(int id)
        {
            return await _context.Students
         .Include(s => s.Enrollments)
             .ThenInclude(e => e.Course)
         .AsNoTracking()
         .FirstOrDefaultAsync(m => m.StudentId == id);
        }
    }
}