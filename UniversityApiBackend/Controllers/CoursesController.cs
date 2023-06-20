using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Repositories;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly CourseRepository _courseRepository;

        public CoursesController(UniversityDBContext context, CourseRepository courseRepository)
        {
            _context = context;
            _courseRepository = courseRepository;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
          if (_context.Courses == null)
          {
              return Problem("Entity set 'UniversityDBContext.Courses'  is null.");
          }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: api/Courses/level
        [HttpGet("level")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByLevel(CourseLevel level)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses.Where(c => c.Level == level && c.Students.Count > 0).ToListAsync();

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        [HttpGet("SearchByLevelAndCategory")]
        public async Task<IActionResult> GetCoursesByLevelAndCategory([FromQuery] CourseLevel level, [FromQuery] string category)
        {
            try
            {
                var courses = await _courseRepository.GetCoursesByLevelAndCategory(level, category);

                if (courses == null)
                    return NotFound();

                return Ok(courses);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Courses/emptyCourses
        [HttpGet("emptyCourses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetEmptyCourses()
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses.Where(c => c.Students.Count == 0).ToListAsync();

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }
    }
}
