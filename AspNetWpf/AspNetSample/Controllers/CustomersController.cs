using AspNetSample.Repository;
using Microsoft.AspNetCore.Mvc;
using SharedDTOs.Models;
using System.Threading.Tasks;

namespace AspNetSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerRepository _repository;

        public CustomersController(CustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            var customers = await _repository.GetAsync();
            return Ok(customers);
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<CustomerDto>> GetByCode(string code)
        {
            var customer = await _repository.GetByCodeAsync(code);

            if(customer is null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CustomerDto customerDto)
        {
            await _repository.InsertAsync(customerDto);
            return Ok(customerDto);
        }

        [HttpDelete("{customerCode}")]
        public async Task<IActionResult> Delete(string customerCode)
        {
            var result = await _repository.DeleteAsync(customerCode);
            return Ok(result);
        }
    }
}
