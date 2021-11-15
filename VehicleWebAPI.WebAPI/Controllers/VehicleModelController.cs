using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleWebAPI.Model;
using VehicleWebAPI.Service;
using VehicleWebAPI.Service.Common;

namespace VehicleWebAPI.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {
        private readonly IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel> _service;
        private readonly IMapper _mapper;

        public VehicleModelController(IVehicleGenericService<IVehicleModelDataModel, VehicleModelDataModel> service,
                                    IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/[VehicleMake]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleModelViewModel>>> GetVehicleModelDataModel([FromQuery] QueryModel model)//get all, page, sort, filter
        {
            var filtering = _mapper.Map<FilteringModelModel>(model);
            var sorting = _mapper.Map<SortingModelModel>(model);
            var paging = _mapper.Map<PagingModelModel>(model);
            var dataList = await _service.FindDataAsync(filtering, sorting, paging);
            var viewlist = _mapper.Map<IEnumerable<VehicleModelViewModel>>(dataList);
            return (List<VehicleModelViewModel>) viewlist;
        }

        // GET: api/VehicleMake/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModelViewModel>> GetVehicleModelDataModel(int id)//read by id
        {
            try
            {
                var vehicleModelDataModel = await _service.ReadItemAsync(id);
                var vehicleModelViewModel = _mapper.Map<VehicleModelViewModel>(vehicleModelDataModel);
                return (VehicleModelViewModel)vehicleModelViewModel;
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
        }

        // PUT: api/VehicleMake/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleModelDataModel(int id, VehicleModelViewModel vehicleModelViewModel)//update
        {
            if (id != vehicleModelViewModel.Id)
            {
                return BadRequest();
            }

            try
            {
                var vehicleModelDataModel = _mapper.Map<VehicleModelDataModel>(vehicleModelViewModel);
                await _service.UpdateItemAsync(vehicleModelDataModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await VehicleModelDataModelExists(id)))
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
        public async Task<ActionResult<VehicleModelViewModel>> PostVehicleModelDataModel(VehicleModelViewModel vehicleModelViewModel)//create new
        {
            var vehicleModelDataModel = _mapper.Map<VehicleModelDataModel>(vehicleModelViewModel);
            await _service.CreateItemAsync(vehicleModelDataModel);

            return CreatedAtAction("GetVehicleMakeViewModel", new { id = vehicleModelViewModel.Id }, vehicleModelViewModel);
        }

        // DELETE: api/VehicleMake/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleModelDataModel(int id)//delete
        {
            var res = await _service.DeleteItemAsync(id);
            if(res)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<bool> VehicleModelDataModelExists(int id)
        {
            try
            {
                var data = await _service.ReadItemAsync(id);
                return true;
            }
            catch(NullReferenceException)
            {
                return false;
            }
        }
    }
}
