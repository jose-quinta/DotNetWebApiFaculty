using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class InvestigatorController : ControllerBase {
        private readonly ApplicationDbContext _context;
        public InvestigatorController(ApplicationDbContext context) => _context = context;
        [HttpGet]
        public async Task<ActionResult<List<Investigator>>> Get() {
            var response = await _context.Researchers.ToListAsync();

            if (response == null)
                return BadRequest($"There not are researchers yet");

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Investigator>> GetById(int id) {
            var response = await _context.Researchers.FindAsync(id);

            if (response == null)
                return BadRequest($"Investigator does not exist or is {response}");

            return Ok(response);
        }
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<EquipmentsName>> GetNamesByName(string name) {
            var _response = await _context.Researchers.Where(x => x.Name == name).FirstOrDefaultAsync();
            var _equipments = await _context.Equipments.Where(x => x.InvestigatorId == _response!.Id).ToListAsync();

            var response = new EquipmentsName() {
                Name = _response!.Name
            };
            foreach (var item in _equipments) {
                response.Names.Add(item.Description);
            }

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<List<Investigator>>> Post(InvestigatorDto request) {
            if (request == null)
                return BadRequest($"Investagator data is empty or is {request}");

            var _request = new Investigator() {
                DNI = request.DNI,
                Name = request.Name,
                Lastname = request.Lastname,
                FacultyId = request.FacultyId
            };

            await _context.AddAsync(_request);
            await _context.SaveChangesAsync();

            var response = await _context.Researchers.ToListAsync();

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Investigator>> Put(int id, InvestigatorDto request) {
            if (request ==  null)
                return BadRequest($"Investigator data is empty or is {request}");

            var response = await _context.Researchers.FindAsync(id);

            if (response ==  null)
                return BadRequest($"The investigator does not exist or is {response}");

            response.DNI = request.DNI;
            response.Name = request.Name;
            response.Lastname = request.Lastname;
            response.FacultyId = request.FacultyId;

            await _context.SaveChangesAsync();

            return Ok(response);
        }
        [HttpDelete("{id}", Name = nameof(DeleteById))]
        public async Task<ActionResult<Investigator>> DeleteById(int id) {
            var response = await _context.Researchers.FindAsync(id);

            if (response == null)
                return BadRequest($"Investigator does not exist or is {response}");

            _context.Remove(response);
            await _context.SaveChangesAsync();

            return Ok(response);
        }
        [HttpDelete("ByName/{name}", Name = nameof(DeleteByName))]
        public async Task<ActionResult<Investigator>> DeleteByName(string name) {
            var response = await _context.Researchers.Where(x => x.Name == name).FirstOrDefaultAsync();

            if (response == null)
                return BadRequest($"Investigator does not exist or is {response}");

            _context.Remove(response);
            await _context.SaveChangesAsync();

            return Ok(response);
        }
    }
}