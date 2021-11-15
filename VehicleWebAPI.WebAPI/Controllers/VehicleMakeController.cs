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
    public class VehicleMakeController : ControllerBase
    {
        private readonly IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel> _service;
        private readonly IMapper _mapper;

        public VehicleMakeController(IVehicleGenericService<IVehicleMakeDataModel, VehicleMakeDataModel> service,
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
            var dataList = await _service.FindDataAsync(filtering, sorting, paging);
            var viewlist = _mapper.Map<IEnumerable<VehicleMakeViewModel>>(dataList);
            return (List<VehicleMakeViewModel>) viewlist;
        }

        // GET: api/VehicleMake/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMakeViewModel>> GetVehicleMakeDataModel(int id)//read by id
        {
            try
            {
                var vehicleMakeDataModel = await _service.ReadItemAsync(id);
                var vehicleMakeViewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMakeDataModel);
                return (VehicleMakeViewModel)vehicleMakeViewModel;
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
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
                var vehicleMakeDataModel = _mapper.Map<VehicleMakeDataModel>(vehicleMakeViewModel);
                await _service.UpdateItemAsync(vehicleMakeDataModel);
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
            var vehicleMakeDataModel = _mapper.Map<VehicleMakeDataModel>(vehicleMakeViewModel);
            await _service.CreateItemAsync(vehicleMakeDataModel);

            return CreatedAtAction("GetVehicleMakeViewModel", new { id = vehicleMakeViewModel.Id }, vehicleMakeViewModel);
        }

        // DELETE: api/VehicleMake/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMakeDataModel(int id)//delete
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

        private async Task<bool> VehicleMakeDataModelExists(int id)
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
