using Microsoft.AspNetCore.Mvc;
using SharedDTOs.Models;

namespace AspNetSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private static readonly List<CustomerDto> mockData = new()
        {
            new CustomerDto("001", "取引先01", "トリヒキサキ01", "東京都"),
            new CustomerDto("002", "取引先02", "トリヒキサキ02", "大阪府"),
        };

        [HttpGet]
        public IEnumerable<CustomerDto> Get() => mockData;

        [HttpPost]
        public IActionResult Post(CustomerDto dto)
        {
            mockData.Add(dto);
            return CreatedAtAction(nameof(Get), new { code = dto.Code }, dto);
        }
    }
}
