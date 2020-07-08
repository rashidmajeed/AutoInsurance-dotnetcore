
using AutoInsurance.API.DTOs;
using AutoInsurance.API.Helper;
using AutoInsurance.API.Models;
using AutoInsurance.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly AutoInsuranceContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly string containerName = "customers";

        public CustomersController(AutoInsuranceContext context,
            IMapper mapper,
            IFileStorageService fileStorageService)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Customers.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var customer = await queryable.Paginate(pagination).ToListAsync();
            return mapper.Map<List<CustomerDTO>>(customer);
        }

        [HttpGet("{id}", Name = "getCustomer")]
        public async Task<ActionResult<CustomerDTO>> Get(int id)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return mapper.Map<CustomerDTO>(customer);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CustomerCreationDTO customerCreationDTO)
        {
            var customer = mapper.Map<Customer>(customerCreationDTO);

            if (customerCreationDTO.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await customerCreationDTO.Image.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(customerCreationDTO.Image.FileName);
                    customer.Image =
                        await fileStorageService.SaveFile(content, extension, containerName,
                                                            customerCreationDTO.Image.ContentType);
                }
            }

            context.Add(customer);
            await context.SaveChangesAsync();
            var customerDTO = mapper.Map<CustomerDTO>(customer);
            return new CreatedAtRouteResult("getCustomer", new { id = customer.Id }, customerDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] CustomerCreationDTO customerCreationDTO)
        {
            var customerDB = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customerDB == null) { return NotFound(); }

            customerDB = mapper.Map(customerCreationDTO, customerDB);

            if (customerCreationDTO.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await customerCreationDTO.Image.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(customerCreationDTO.Image.FileName);
                    customerDB.Image =
                        await fileStorageService.EditFile(content, extension, containerName,
                                                            customerDB.Image,
                                                            customerCreationDTO.Image.ContentType);
                }
            }

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<CustomerPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entityFromDB = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (entityFromDB == null)
            {
                return NotFound();
            }

            var entityDTO = mapper.Map<CustomerPatchDTO>(entityFromDB);

            patchDocument.ApplyTo(entityDTO, ModelState);

            var isValid = TryValidateModel(entityDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(entityDTO, entityFromDB);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Customers.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Customer() { Id = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}