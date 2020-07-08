using System.Collections.Generic;
using System.Threading.Tasks;
using AutoInsurance.API.DTOs;
using AutoInsurance.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoInsurance.API.Controllers
{
       [Route("api/claims")]
    [ApiController]
    public class ClaimsController: ControllerBase
    {
        private readonly ILogger<ClaimsController> logger;
        private readonly AutoInsuranceContext context;
        private readonly IMapper mapper;

        public ClaimsController(ILogger<ClaimsController> logger,
            AutoInsuranceContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet] // api/claims
        public async Task<ActionResult<List<ClaimDTO>>> Get()
        {
            var claims = await context.Claims.AsNoTracking().ToListAsync();
            var claimsDTOs = mapper.Map<List<ClaimDTO>>(claims);
            return claimsDTOs;
        }

        [HttpGet("{Id:int}", Name = "getClaims")] // api/claims/example
        public async Task<ActionResult<ClaimDTO>> Get(int Id)
        {
            var claim = await context.Claims.FirstOrDefaultAsync(x => x.Id == Id);

            if (claim == null)
            {
                return NotFound();
            }

            var claimDTO = mapper.Map<ClaimDTO>(claim);

            return claimDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClaimCreationDTO claimCreation)
        {
            var claim = mapper.Map<Claim>(claimCreation);
            context.Add(claim);
            await context.SaveChangesAsync();
            var claimDTO = mapper.Map<ClaimDTO>(claim);

            return new CreatedAtRouteResult("getClaim", new { claimDTO.Id }, claimDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ClaimCreationDTO claimCreation)
        {
            var claim = mapper.Map<Claim>(claimCreation);
            claim.Id = id;
            context.Entry(claim).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Claims.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Claim() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
} 
    
