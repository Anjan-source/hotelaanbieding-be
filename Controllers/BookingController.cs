using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hotelaanbieding_be.Controllers
{
    [Route("api/[Booking]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Creates a new instance of the <see cref="BookingController"/> class.
        /// </summary>
        public BookingController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<HotelManagementController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<HotelManagementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HotelManagementController>
        [HttpPost]
        public void Post(CreateBookingCommand bookingCommand)
        {
            return await mediator.Send<CustomQueryResult<CreateBookingCommand>>(
                query,
                CancellationToken.None);
        }

        // PUT api/<HotelManagementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HotelManagementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
