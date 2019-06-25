using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourses.Data;
using StudentCourses.Data.Repository;

namespace StudentCourses.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
       
        private readonly IStudentRepository _repository;
        public StudentsController(IStudentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _repository.GetAll();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _repository.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // GET: api/Students/5
        [HttpGet("{id}/Courses")]
        public async Task<ActionResult<Student>> GetStudentCourses(int id)
        {
            var student = await _repository.GetCoursesById(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }


        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(student);            
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
           await _repository.Insert(student);
         
            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _repository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }


            await _repository.Delete(id);
           

            return student;
        }

        private bool StudentExists(int id)
        {
            return _repository.GetById(id) != null;
        }
    }
}
