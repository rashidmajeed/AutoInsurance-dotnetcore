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
    [Route("api/coverages")]
    [ApiController]
    public class CoveragesController : ControllerBase
    {
        private readonly ILogger<CoveragesController> logger;
        private readonly AutoInsuranceContext context;
        private readonly IMapper mapper;

        public CoveragesController(ILogger<CoveragesController> logger,
            AutoInsuranceContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet] // api/coverages
        [EnableCors(PolicyName = "AllowAPIRequestIO")]
        public async Task<ActionResult<List<CoverageDTO>>> Get()
        {
            var coverages = await context.Coverages.AsNoTracking().ToListAsync();
            var coveragesDTOs = mapper.Map<List<CoverageDTO>>(coverages);
            return coveragesDTOs;
        }

        [HttpGet("{Id:int}", Name = "getCoverage")] // api/coverages/example
        public async Task<ActionResult<CoverageDTO>> Get(int Id)
        {
            var coverage = await context.Coverages.FirstOrDefaultAsync(x => x.Id == Id);

            if (coverage == null)
            {
                return NotFound();
            }

            var coverageDTO = mapper.Map<CoverageDTO>(coverage);

            return coverageDTO;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] CoverageCreationDTO coverageCreation)
        {
            var coverage = mapper.Map<Coverage>(coverageCreation);
            context.Add(coverage);
            await context.SaveChangesAsync();
            var coverageDTO = mapper.Map<CoverageDTO>(coverage);

            return new CreatedAtRouteResult("getCoverage", new { coverageDTO.Id }, coverageDTO);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<ActionResult> Put(int id, [FromBody] CoverageCreationDTO coverageCreation)
        {
            var coverage = mapper.Map<Coverage>(coverageCreation);
            coverage.Id = id;
            context.Entry(coverage).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Coverages.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Coverage() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}

