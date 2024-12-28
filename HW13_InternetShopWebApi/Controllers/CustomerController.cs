
using HW13_InternetShop.Data;
using HW13_InternetShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using HW13_InternetShop._Contract.QueryModels;

namespace HW13_InternetShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly IShopServices<Customer> _customerService;

        public CustomerController(IShopServices<Customer> customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allCustomers = _customerService.GetAll();

            if(allCustomers is not null  && allCustomers.Count() > 0)
                return Ok(allCustomers);
            else
                return NotFound();
        }

        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _customerService.GetById(id);

            if (result is not null)
                return Ok(result);
            else 
                return NotFound();
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] Customer value)
        {
            if(value is not null)
                return Ok(_customerService.Add(value));
            else
                return BadRequest();
        }
        
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer value)
        {
            
            if (value is not null)
            {
                return Ok(_customerService.Update(id, value));
            }
            else
            {
                return BadRequest();
            }
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _customerService.Delete(id);

            if(isDeleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
