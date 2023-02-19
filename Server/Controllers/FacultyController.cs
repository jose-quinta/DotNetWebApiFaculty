using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class FacultyController : ControllerBase {
        private readonly ApplicationDbContext _context;
        public FacultyController(ApplicationDbContext context) => _context = context;
        [HttpGet]
        public async Task<ActionResult<List<Faculty>>> Get() {
            var response = await _context.Faculties.ToListAsync();

            if (response == null)
                return BadRequest($"There not are facuties yet");

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Faculty>> GetById(int id) {
            var response = await _context.Faculties.FindAsync(id);

            if (response == null)
                return BadRequest($"The faculty does not exist or is {response}");

            return Ok(response);
        }
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<ResearchersName>> GetNamesByName(string name) {
            var _response = await _context.Faculties.Where(x => x.Name == name).FirstOrDefaultAsync();
            var _researchers = await _context.Researchers.Where(x => x.FacultyId == _response!.Id).ToListAsync();

            var response = new ResearchersName() {
                Name = _response!.Name
            };
            foreach (var item in _researchers) {
                response.Names.Add(item.Name);
            }

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<List<Faculty>>> Post(FacultyDto request) {
            if (request == null)
                return BadRequest($"Faculty data is empty or is {request}");

            var _request = new Faculty() {
                Name = request.Name
            };

            await _context.AddAsync(_request);
            await _context.SaveChangesAsync();

            var response = await _context.Faculties.ToListAsync();

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Faculty>> Put(int id, FacultyDto request) {
            if (request == null)
                return BadRequest($"Faculty data is empty or is {request}");

            var response = await _context.Faculties.FindAsync(id);

            response!.Name = request.Name;
            await _context.SaveChangesAsync();

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Faculty>> Delete(int id) {
            var response = await _context.Faculties.FindAsync(id);

            if (response == null)
                return BadRequest($"The faculty does not exist or is {response}");

            _context.Remove(response);
            await _context.SaveChangesAsync();

            return Ok(response);
        }
    }
}