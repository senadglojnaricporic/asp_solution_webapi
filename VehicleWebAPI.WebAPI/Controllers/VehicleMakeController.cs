using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWebAPI.Model;
using VehicleWebAPI.Model.Common;
using VehicleWebAPI.Service;
using VehicleWebAPI.Service.Common;

namespace VehicleWebAPI.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakeController : ControllerBase
    {
        private readonly IVehicleMakeService<IVehicleMakeGenericModel<VehicleModelViewModel>> _service;
        private readonly IMapper _mapper;

        public VehicleMakeController(IVehicleMakeService<IVehicleMakeGenericModel<VehicleModelViewModel>> service,
                                    IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/[VehicleMake]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMakeViewModel>>> GetVehicleMakeDataModel([FromQuery] QueryModel model)//get all, page, sort, filter
        {
            var filtering = _mapper.Map<FilteringMakeModel>(model);
            var sorting = _mapper.Map<SortingMakeModel>(model);
            var paging = _mapper.Map<PagingMakeModel>(model);
            return (List<VehicleMakeViewModel>) await _service.GetPageAsync(filtering, sorting, paging);
        }

        // GET: api/VehicleMake/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMakeViewModel>> GetVehicleMakeDataModel(int id)//read by id
        {
            var vehicleMakeViewModel = await _service.ReadItemAsync(id);

            if (vehicleMakeViewModel == null)
            {
                return NotFound();
            }

            return (VehicleMakeViewModel)vehicleMakeViewModel;
        }

        // PUT: api/VehicleMake/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleMakeDataModel(int id, VehicleMakeViewModel vehicleMakeViewModel)//update
        {
            if (id != vehicleMakeViewModel.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateItemAsync(vehicleMakeViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await VehicleMakeDataModelExists(id)))
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
            await _service.CreateItemAsync(vehicleMakeViewModel);

            return CreatedAtAction("GetVehicleMakeViewModel", new { id = vehicleMakeViewModel.Id }, vehicleMakeViewModel);
        }

        // DELETE: api/VehicleMake/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMakeDataModel(int id)//delete
        {
            var success = await _service.DeleteItemAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        private async Task<bool> VehicleMakeDataModelExists(int id)
        {
            return await _service.ReadItemAsync(id) != null;
        }
    }
}
