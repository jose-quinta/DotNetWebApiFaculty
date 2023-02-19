using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public EquipmentController(ApplicationDbContext context) => _context = context;
        [HttpGet]
        public async Task<ActionResult<List<Equipment>>> Get() {
            var response = await _context.Equipments.ToListAsync();

            if (response == null)
                return BadRequest($"There not are equipments yet");

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> GetById(int id) {
            var response = await _context.Equipments.FindAsync(id);

            if (response == null)
                return BadRequest($"The equipment does not exist or is {response}");

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<List<Equipment>>> Post(EquipmentDto request) {
            if (request == null)
                return BadRequest($"Equipment data is empty or is {request}");

            var _request = new Equipment() {
                Description = request.Description,
                Budget = request.Budget,
                FacultyId = request.FacultyId,
                InvestigatorId = request.InvestigatorId
            };

            await _context.AddAsync(_request);
            await _context.SaveChangesAsync();

            var response = await _context.Equipments.ToListAsync();

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Equipment>> Put(int id, EquipmentDto request) {
            if (request == null)
                return BadRequest($"Equipment data is empty or is {request}");

            var response = await _context.Equipments.FindAsync(id);

            if (response == null)
                return BadRequest($"The equipment does not exist or is {response}");

            response.Description = request.Description;
            response.Budget = request.Budget;
            response.FacultyId = request.FacultyId;
            response.InvestigatorId = request.InvestigatorId;

            await _context.SaveChangesAsync();

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Equipment>> Delete(int id) {
            var response = await _context.Equipments.FindAsync(id);

            if (response == null)
                return BadRequest($"Equipment does not exist or is {response}");

            _context.Remove(response);
            await _context.SaveChangesAsync();

            return Ok(response);
        }
    }
}