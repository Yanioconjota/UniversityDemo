using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;
using static UniversityApiBackend.DataAccess.UniversityDBContext;

namespace UniversityApiBackend.Repositories
{
    public class CourseRepository
    {

        private readonly UniversityDBContext _context;

        public CourseRepository(UniversityDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetCoursesByLevelAndCategory(CourseLevel level, string categoryName)
        {
            if (_context.Courses == null)
            {
                throw new InvalidOperationException("Courses collection is null");
            }

            return await _context.Courses
                .Include(course => course.Categories)
                .Where(course => course.Level == level && course.Categories.Any(category => category.Name == categoryName))
                .ToListAsync();
        }
    }
}
