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
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> logger;
        private readonly AutoInsuranceContext context;
        private readonly IMapper mapper;

        public PaymentsController(ILogger<PaymentsController> logger,
            AutoInsuranceContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet] // api/payments
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<List<PaymentDTO>>> Get()
        {
            var payments = await context.Payments.AsNoTracking().ToListAsync();
            var paymentsDTOs = mapper.Map<List<PaymentDTO>>(payments);
            return paymentsDTOs;
        }

        [HttpGet("{Id:int}", Name = "getPayments")] // api/payments/example
         [ProducesResponseType(404)]
        [ProducesResponseType(typeof(PaymentDTO), 200)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<ActionResult<PaymentDTO>> Get(int Id)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(x => x.Id == Id);

            if (payment == null)
            {
                return NotFound();
            }

            var paymentDTO = mapper.Map<PaymentDTO>(payment);

            return paymentDTO;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<ActionResult> Post([FromBody] PaymentCreationDTO paymentCreation)
        {
            var payment = mapper.Map<Payment>(paymentCreation);
            context.Add(payment);
            await context.SaveChangesAsync();
            var paymentDTO = mapper.Map<PaymentDTO>(payment);

            return new CreatedAtRouteResult("getPayment", new { paymentDTO.Id }, paymentDTO);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> Put(int id, [FromBody] PaymentCreationDTO paymentCreation)
        {
            var payment = mapper.Map<Payment>(paymentCreation);
            payment.Id = id;
            context.Entry(payment).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

         /// <summary>
        /// Delete a payment
        /// </summary>
        /// <param name="id">Id of the payment to delete</param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Payments.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Payment() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}

