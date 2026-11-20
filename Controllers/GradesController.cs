using Laboratorio13.DTO.Request;
using Laboratorio13.DTO.Response;
using Laboratorio13.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorio13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/grades  -> lista solo los activos
        [HttpGet]
        public ActionResult<List<GradeResponseV1>> GetAll()
        {
            var response = _context.Grades
                .Where(x => x.IsActive)
                .Select(x => new GradeResponseV1
                {
                    GradeId = x.GradeId,
                    Name = x.Name,
                    Description = x.Description
                }).ToList();

            return Ok(response);
        }

        // GET api/grades/5
        [HttpGet("{id}")]
        public ActionResult<GradeResponseV1> GetById(int id)
        {
            var model = _context.Grades.FirstOrDefault(x => x.GradeId == id && x.IsActive);
            if (model == null) return NotFound();

            // Convertir model a response
            var response = new GradeResponseV1
            {
                GradeId = model.GradeId,
                Name = model.Name,
                Description = model.Description
            };

            return Ok(response);
        }

        // POST api/grades
        [HttpPost]
        public ActionResult<GradeResponseV1> Create(GradeRequestV1 request)
        {
            // Convertir request a model
            var model = new Grade
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = true
            };

            _context.Grades.Add(model);
            _context.SaveChanges();

            var response = new GradeResponseV1
            {
                GradeId = model.GradeId,
                Name = model.Name,
                Description = model.Description
            };

            return CreatedAtAction(nameof(GetById), new { id = model.GradeId }, response);
        }

        // PUT api/grades/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, GradeRequestV1 request)
        {
            var model = _context.Grades.FirstOrDefault(x => x.GradeId == id && x.IsActive);
            if (model == null) return NotFound();

            model.Name = request.Name;
            model.Description = request.Description;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/grades/5  -> eliminación lógica
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _context.Grades.FirstOrDefault(x => x.GradeId == id && x.IsActive);
            if (model == null) return NotFound();

            model.IsActive = false;
            _context.SaveChanges();

            return NoContent();
        }
    }
}