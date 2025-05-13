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
            var trips = await _service.GetDeliveries(id);
            if (trips.Count == 0)
            {
                return NotFound("Brak danego ID.");
            }
            return Ok(trips);
        }
        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] PostDTO client)
        {
            var id = await _service.AddProduct(client);
            if (id == null)
            {
                return BadRequest("Nieprawidłowe dane wejściowe.");
            }
            return CreatedAtAction(nameof(GetTripsForClient), new { id = id.Value }, new { id = id.Value });
        }

    }
    
    
    
    
}


