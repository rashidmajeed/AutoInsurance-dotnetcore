using System.Collections.Generic;
using System.Threading.Tasks;
using AutoInsurance.API.DTOs;
using AutoInsurance.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<ActionResult<List<CustClaimDTO>>> Get()
        {
            var claims = await context.CustClaims.AsNoTracking().ToListAsync();
            var claimsDTOs = mapper.Map<List<CustClaimDTO>>(claims);
            return claimsDTOs;
        }

        [HttpGet("{Id:int}", Name = "getClaims")] // api/claims/example
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(CustClaimDTO), 200)]

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<ActionResult<CustClaimDTO>> Get(int Id)
        {
            var claim = await context.CustClaims.FirstOrDefaultAsync(x => x.Id == Id);

            if (claim == null)
            {
                return NotFound();
            }

            var claimDTO = mapper.Map<CustClaimDTO>(claim);

            return claimDTO;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<ActionResult> Post([FromBody] ClaimCreationDTO claimCreation)
        {
            var claim = mapper.Map<CustClaim>(claimCreation);
            context.Add(claim);
            await context.SaveChangesAsync();
            var claimDTO = mapper.Map<CustClaimDTO>(claim);

            return new CreatedAtRouteResult("getClaim", new { claimDTO.Id }, claimDTO);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<ActionResult> Put(int id, [FromBody] ClaimCreationDTO claimCreation)
        {
            var claim = mapper.Map<CustClaim>(claimCreation);
            claim.Id = id;
            context.Entry(claim).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

         /// <summary>
        /// Delete a claim
        /// </summary>
        /// <param name="id">Id of the claim to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.CustClaims.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new CustClaim() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
} 
    
