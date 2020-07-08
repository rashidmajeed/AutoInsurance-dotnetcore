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
       [Route("api/policies")]
    [ApiController]
    public class PoliciesController: ControllerBase
    {
        private readonly ILogger<PoliciesController> logger;
        private readonly AutoInsuranceContext context;
        private readonly IMapper mapper;

        public PoliciesController(ILogger<PoliciesController> logger,
            AutoInsuranceContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet] // api/policies
        public async Task<ActionResult<List<PolicyDTO>>> Get()
        {
            var policies = await context.Policies.AsNoTracking().ToListAsync();
            var policiesDTOs = mapper.Map<List<PolicyDTO>>(policies);
            return policiesDTOs;
        }

        [HttpGet("{Id:int}", Name = "getPolicy")] // api/policies/example
        public async Task<ActionResult<PolicyDTO>> Get(int Id)
        {
            var policy = await context.Policies.FirstOrDefaultAsync(x => x.Id == Id);

            if (policy == null)
            {
                return NotFound();
            }

            var policyDTO = mapper.Map<PolicyDTO>(policy);

            return policyDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PolicyCreationDTO policyCreation)
        {
            var policy = mapper.Map<Policy>(policyCreation);
            context.Add(policy);
            await context.SaveChangesAsync();
            var policyDTO = mapper.Map<PolicyDTO>(policy);

            return new CreatedAtRouteResult("getPolicy", new { policyDTO.Id }, policyDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PolicyCreationDTO policyCreation)
        {
            var policy = mapper.Map<Policy>(policyCreation);
            policy.Id = id;
            context.Entry(policy).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Policies.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Policy() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
} 
    
