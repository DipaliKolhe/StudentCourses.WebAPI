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
    public class CoursesController : ControllerBase
    {
        private readonly IGenericRepository<Course> _repository;

        public CoursesController(IGenericRepository<Course> repository)
        {
            _repository = repository;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _repository.GetAll();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _repository.GetById(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

        
            try
            {
                await _repository.Update(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            await _repository.Insert(course);
         
            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _repository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);
         
            return course;
        }

        private bool CourseExists(int id)
        {
            return  _repository.GetById(id) != null;
        }
    }
}
