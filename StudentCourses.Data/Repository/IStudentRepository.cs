using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourses.Data.Repository
{
    public interface IStudentRepository: IGenericRepository<Student>
    {
        Task<Student> GetCoursesById(int id);
    }
}
