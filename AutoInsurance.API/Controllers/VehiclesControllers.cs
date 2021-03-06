using System.Collections.Generic;
using System.Threading.Tasks;
using AutoInsurance.API.DTOs;
using AutoInsurance.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoInsurance.API.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> logger;
        private readonly AutoInsuranceContext context;
        private readonly IMapper mapper;

        public VehiclesController(ILogger<VehiclesController> logger,
            AutoInsuranceContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet] // api/vehicles
        [EnableCors(PolicyName = "AllowAPIRequestIO")]
        public async Task<ActionResult<List<VehicleDTO>>> Get()
        {
            var vehicles = await context.Vehicles.AsNoTracking().ToListAsync();
            var vehiclesDTOs = mapper.Map<List<VehicleDTO>>(vehicles);
            return vehiclesDTOs;
        }

        [HttpGet("{Id:int}", Name = "getVehicle")] // api/vehicles/example
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(VehicleDTO), 200)]

        public async Task<ActionResult<VehicleDetailsDTO>> Get(int Id)
        {
            var vehicle = await context.Vehicles
            .Include(x => x.VehicleCoverage).ThenInclude(x => x.Coverage)
            .FirstOrDefaultAsync(x => x.Id == Id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var vehicleDTO = mapper.Map<VehicleDetailsDTO>(vehicle);

            return vehicleDTO;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] VehicleCreationDTO vehicleCreation)
        {
            var vehicle = mapper.Map<Vehicle>(vehicleCreation);
            context.Add(vehicle);
            await context.SaveChangesAsync();
            var vehicleDTO = mapper.Map<VehicleDTO>(vehicle);

            return new CreatedAtRouteResult("getVehicle", new { vehicleDTO.Id }, vehicleDTO);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<ActionResult> Put(int id, [FromBody] VehicleCreationDTO vehicleCreation)
        {
            var vehicle = mapper.Map<Vehicle>(vehicleCreation);
            vehicle.Id = id;
            context.Entry(vehicle
            ).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

         /// <summary>
        /// Delete a vehicle
        /// </summary>
        /// <param name="id">Id of the vehicle to delete</param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Vehicles.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Vehicle() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}

