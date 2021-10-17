using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWebAPI.DAL;
using VehicleWebAPI.Model;

namespace VehicleWebAPI.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakeController : ControllerBase
    {
        private readonly VehicleWebAPIDbContext _context;

        public VehicleMakeController(VehicleWebAPIDbContext context)
        {
            _context = context;
        }

        // GET: api/VehicleMake
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMakeDataModel>>> GetVehicleMakeDataModel()//get all, page, sort, filter
        {
            return await _context.VehicleMakeDataModel.ToListAsync();
        }

        // GET: api/VehicleMake/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMakeViewModel>> GetVehicleMakeDataModel(int id)//read by id
        {
            var vehicleMakeDataModel = await _context.VehicleMakeDataModel.FindAsync(id);

            if (vehicleMakeDataModel == null)
            {
                return NotFound();
            }

            return vehicleMakeDataModel;
        }

        // PUT: api/VehicleMake/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleMakeDataModel(int id, VehicleMakeDataModel vehicleMakeDataModel)//update
        {
            if (id != vehicleMakeDataModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleMakeDataModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleMakeDataModelExists(id))
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

        // POST: api/VehicleMake
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleMakeViewModel>> PostVehicleMakeDataModel(VehicleMakeViewModel vehicleMakeViewModel)//create new
        {
            _context.VehicleMakeDataModel.Add(vehicleMakeViewModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleMakeViewModel", new { id = vehicleMakeViewModel.Id }, vehicleMakeViewModel);
        }

        // DELETE: api/VehicleMake/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMakeDataModel(int id)//delete
        {
            var vehicleMakeDataModel = await _context.VehicleMakeDataModel.FindAsync(id);
            if (vehicleMakeDataModel == null)
            {
                return NotFound();
            }

            _context.VehicleMakeDataModel.Remove(vehicleMakeDataModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleMakeDataModelExists(int id)
        {
            return _context.VehicleMakeDataModel.Any(e => e.Id == id);
        }
    }
}
