using APBD_example_test1_2025.Exceptions;
using APBD_example_test1_2025.Models.DTOs;
using APBD_example_test1_2025.Services;
using Microsoft.AspNetCore.Mvc;


namespace APBD_example_test1_2025.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDbService _service;
        public DeliveriesController(IDbService service) => _service = service;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripsForClient(int id)
        {
            var dev = await _service.GetDeliveries(id);
            if (dev.Count == 0)
            {
                return NotFound("Brak danego ID.");
            }
            return Ok(dev);
        }
        [HttpPost]
        public async Task<IActionResult> AddDelivery([FromBody] PostDTO client)
        {
            var id = await _service.AddDelivery(client);
            if (id == null)
            {
                return BadRequest("Nieprawidłowe dane wejściowe.");
            }
            return CreatedAtAction(nameof(GetTripsForClient), new { id = id.Value }, new { id = id.Value });
        }

    }
    
    
    
    
}


