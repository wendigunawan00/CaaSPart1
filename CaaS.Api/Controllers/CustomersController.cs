using AutoMapper;
using CaaS.DTO;
using Microsoft.AspNetCore.Mvc;
//using CaaS.Api.BackgroundServices;
using Dal.Common;
using CaaS.Logic;
using CaaS.Domain;
using Microsoft.AspNetCore.Authorization;
using MySqlX.XDevAPI.Relational;
using CaaS.Dal.Ado;
using Microsoft.IdentityModel.Tokens;

namespace CaaS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(WebApiConventions))]
    public class CustomersController : ControllerBase
    {
        private readonly IManagementLogic<Person> logic;
        private readonly IManagementLogic<Address> logicAddress;
        private readonly IMapper mapper;


        public CustomersController(IManagementLogic<Person> logic, IMapper mapper)
        {           
            this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
                
        
        private async Task<PersonDTO?> GetLast()
        {
            var Persons = await logic.Get();          
            return mapper.Map<PersonDTO>(Persons.LastOrDefault());
        }        
        
       
        /// <summary>
        /// Returns a Person by PersonID.
        /// </summary>
        /// <param name="PersonId">ID</param>      
        /// <returns>The Person with the given ID</returns>
        [HttpGet("{PersonId}")]
        public async Task<ActionResult<PersonDTO>> GetPersonById(string PersonId)
        {
            Person? prod = await logic.Search(PersonId);
            if (prod is null)
            {
                return NotFound(StatusInfo.InvalidPersonId(PersonId));
            }
            //return Ok(Person.ToDto());
            return mapper.Map<PersonDTO>(prod);
        }

        /// <summary>
        /// Returns a list of Persons
        /// </summary>
        /// <returns>a list of Persons</returns>
        [HttpGet]
        public async Task<IEnumerable<PersonDTO>> GetPersons()
        {
            var Persons = await logic.Get();
            return mapper.Map<IEnumerable<PersonDTO>>(Persons);
        }

        /// <summary>
        /// Returns a created Person with the id after creating a Person
        /// </summary>
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<PersonDTO>> CreatePerson([FromBody] PersonForCreationDTO PersonDTO, [FromBody] AddressForCreationDTO AddressDTO)
        {         
            Domain.Person Person = mapper.Map<Domain.Person>(PersonDTO);
            Domain.Address newAddress = mapper.Map<Domain.Address>(AddressDTO);
            var count = await logic.CountAll();
       
            Person.Id = $"cust{count+1}";
            Person.AddressId = $"addr-cust{count+1}";
            newAddress.Id = Person.AddressId;
            await logic.Add(Person);
            await logicAddress.Add(newAddress);
            return CreatedAtAction(actionName: nameof(GetPersonById),
                routeValues: new { PersonId = Person.Id },
                //value: Person.ToDto()
                value: mapper.Map<PersonDTO>(Person)
                );
        }

        /// <summary>
        /// Update a Person with the Person id given and show it back if success updating
        /// </summary>
        [HttpPut("{PersonId}")]
        //[Authorize]
        public async Task<ActionResult<PersonDTO>> UpdatePerson(string PersonId,[FromBody] PersonDTO PersonDTO, [FromBody] AddressDTO newAddressDTO)
        {
            Domain.Person? Person = (Person?) await logic.Search(PersonId);

            if (Person is null) {
                return NotFound();
            }
            Domain.Address newAddress = mapper.Map<Domain.Address>(newAddressDTO);

            mapper.Map(PersonDTO,Person);

            await logic.Update(Person);
            await logicAddress.Add(newAddress);

            return Ok("Finished Updating");
        }


        /// <summary>
        /// Delete a Person with the Person-id given
        /// </summary>
        [HttpDelete("{PersonId}")]
        //[Authorize]
        public async Task<ActionResult> DeletePerson([FromRoute] String PersonId)
        {
            if (await logic.Delete(PersonId))
            {
                return Ok("Finished Deleting");
            }
            else
            {
                return NotFound();
            }
        }
       
    }
}
