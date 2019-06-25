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
    public class EnrollmentsController : ControllerBase
    {
        private readonly IGenericRepository<Enrollment> _genericRepository;

        public EnrollmentsController(IGenericRepository<Enrollment> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment = await _genericRepository.GetById(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return enrollment;
        }

        // POST: api/Enrollments
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment)
        {
           
            try
            {
                await _genericRepository.Insert(enrollment);
            }
            catch (DbUpdateException)
            {
               
            }
            return CreatedAtAction("GetEnrollment", new { id = enrollment.EnrollmentId }, enrollment);
        }

      
    }
}
