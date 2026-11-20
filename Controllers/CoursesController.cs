using Laboratorio13.DTO.Request;
using Laboratorio13.DTO.Response;
using Laboratorio13.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/courses  -> lista solo los activos
        [HttpGet]
        public ActionResult<List<CourseResponseV1>> GetAll()
        {
            var response = _context.Courses
                .Where(x => x.IsActive)
                .Select(x => new CourseResponseV1
                {
                    CourseId = x.CourseId,
                    Name = x.Name,
                    Credit = x.Credit
                }).ToList();

            return Ok(response);
        }

        // GET api/courses/5
        [HttpGet("{id}")]
        public ActionResult<CourseResponseV1> GetById(int id)
        {
            var model = _context.Courses.FirstOrDefault(x => x.CourseId == id && x.IsActive);
            if (model == null) return NotFound();

            // Convertir model a response
            var response = new CourseResponseV1
            {
                CourseId = model.CourseId,
                Name = model.Name,
                Credit = model.Credit
            };

            return Ok(response);
        }

        // POST api/courses
        [HttpPost]
        public ActionResult<CourseResponseV1> Create(CourseRequestV1 request)
        {
            // Convertir request a model
            var model = new Course
            {
                Name = request.Name,
                Credit = request.Credit,
                IsActive = true
            };

            _context.Courses.Add(model);
            _context.SaveChanges();

            var response = new CourseResponseV1
            {
                CourseId = model.CourseId,
                Name = model.Name,
                Credit = model.Credit
            };

            return CreatedAtAction(nameof(GetById), new { id = model.CourseId }, response);
        }

        // PUT api/courses/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, CourseRequestV1 request)
        {
            var model = _context.Courses.FirstOrDefault(x => x.CourseId == id && x.IsActive);
            if (model == null) return NotFound();

            model.Name = request.Name;
            model.Credit = request.Credit;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/courses/5  -> eliminación lógica
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _context.Courses.FirstOrDefault(x => x.CourseId == id && x.IsActive);
            if (model == null) return NotFound();

            model.IsActive = false;
            _context.SaveChanges();

            return NoContent();
        }
    }
}