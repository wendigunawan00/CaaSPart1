﻿using AutoMapper;
using CaaS.Dal.Interfaces;
using CaaS.Dtos;
using Microsoft.AspNetCore.Mvc;
//using CaaS.Api.BackgroundServices;
using Dal.Common;
using CaaS.Logic;
using CaaS.Domain;

namespace CaaS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(WebApiConventions))]
    public class CartsOrdersController : ControllerBase
    {
        private readonly IOrderManagementLogic<Person  > logic;
        private readonly IMapper mapper;
        //private readonly UpdateChannel updateChannel;

        //public CustomersController( IMapper mapper, UpdateChannel updateChannel, string table)
        public CartsOrdersController(IOrderManagementLogic<Person> logic, IMapper mapper)
        {           
            this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //this.updateChannel = updateChannel ?? throw new ArgumentNullException(nameof(updateChannel));
        }

        //[ProducesDefaultResponseType]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        // so könnte direkt die Methode vorgegeben werden
        //[ApiConventionMethod(typeof(WebApiConventions), nameof(WebApiConventions.Get))]
        [HttpGet]
        public async Task<IEnumerable<Person>> GetPersons()
        {
            var Persons = await logic.Get();
            //if (rating is null)
            //{
            //    customers = await logic.FindAllAsync();
            //}
            //else
            //{
            //    customers = await logic.FindAllAsyncByRating(rating.Value);
            //}
            //return customers.Select(c => c.ToDto());
            //return mapper.Map<IEnumerable<PersonDTO>>(customers);
            return mapper.Map<IEnumerable<Person>>(Persons);
        }

        /// <summary>
        /// Returns a customer by id.
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns>The customer with the given id</returns>
        //[ProducesDefaultResponseType]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[HttpGet("{customerId}")]
        //public async Task<ActionResult<PersonDTO>> GetCustomerById([FromRoute] Guid customerId)
        //{
        //    var customer = await logic.GetCustomer(customerId);
        //    if (customer is null)
        //    {
        //        return NotFound(StatusInfo.InvalidCustomerId(customerId));
        //    }
        //    //return Ok(customer.ToDto());
        //    return mapper.Map<PersonDTO>(customer);
        //}

        ////[ProducesDefaultResponseType]
        ////[ProducesResponseType(StatusCodes.Status201Created)]
        ////[ProducesResponseType(StatusCodes.Status400BadRequest)]
        ////[ProducesResponseType(StatusCodes.Status409Conflict)]
        //[HttpPost]
        //public async Task<ActionResult<PersonDTO>> CreateCustomer([FromBody] CustomerForCreationDto PersonDTO)
        //{
        //    if (PersonDTO.Id != Guid.Empty && await logic.CustomerExists(PersonDTO.Id))
        //    {
        //        return Conflict();
        //    }
        //    //Domain.Customer customer = PersonDTO.ToDomain();
        //    Domain.Customer customer = mapper.Map<Domain.Customer>(PersonDTO);
        //    await logic.AddCustomer(customer);
        //    return CreatedAtAction(actionName: nameof(GetCustomerById),
        //        routeValues: new { customerId = customer.Id },
        //        //value: customer.ToDto()
        //        value: mapper.Map<PersonDTO>(customer)
        //        );
        //}

        ////[ProducesDefaultResponseType]
        ////[ProducesResponseType(StatusCodes.Status204NoContent)]
        ////[ProducesResponseType(StatusCodes.Status400BadRequest)]
        ////[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[HttpPut("{customerId}")]
        //public async Task<ActionResult<PersonDTO>> UpdateCustomer(
        //    Guid customerId,
        //    [FromBody] CustomerForUpdateDto PersonDTO)
        //{
        //    Domain.Customer? customer = await logic.GetCustomer(customerId);
        //    if (customer is null)
        //    {
        //        return NotFound();
        //    }

        //    mapper.Map(PersonDTO, customer);

        //    await logic.UpdateCustomer(customer);
        //    return NoContent();
        //}

        ////[ProducesDefaultResponseType]
        ////[ProducesResponseType(StatusCodes.Status204NoContent)]
        ////[ProducesResponseType(StatusCodes.Status400BadRequest)]
        ////[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[HttpDelete("{customerId}")]
        //public async Task<ActionResult> DeleteCustomer([FromRoute] Guid customerId)
        //{
        //    if (await logic.DeleteCustomer(customerId))
        //    {
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        //[ProducesDefaultResponseType]
        //[ProducesResponseType(StatusCodes.Status202Accepted)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        //[HttpPost("{customerId}/update-totals")]
        //public async Task<ActionResult> UpdateCustomerTotals([FromRoute] Guid customerId)
        //{
        //    if (!await logic.CustomerExists(customerId))
        //    {
        //        return NotFound(StatusInfo.InvalidCustomerId(customerId));
        //    }

        //    //await logic.UpdateTotalRevenue(customerId);
        //    //return NoContent();

        //    if (await updateChannel.AddUpdateTaskAsync(customerId))
        //    {
        //        return Accepted();
        //    }
        //    return new StatusCodeResult(StatusCodes.Status429TooManyRequests);
        //}
    }
}
