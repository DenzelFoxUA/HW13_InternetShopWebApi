using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Services;
using Microsoft.AspNetCore.Mvc;


namespace HW13_InternetShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderIService;

        public OrderController(IOrderServices orderService)
        {
            _orderIService = orderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Order>orders = _orderIService.GetAll();

            if (orders is not null && orders.Count() > 0)
                return Ok(orders);
            else
                return NotFound();
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _orderIService.GetById(id);

            if(order is not null)
            {
                return Ok(order);
            }
            
            return NotFound();
            
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order value)
        {
            if(value is not null)
            {
                
                return Ok(_orderIService.Create(value));
            }
            else
                return BadRequest();
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order value)
        {
            if (value is not null)
            {
                return Ok(_orderIService.Update(id, value));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _orderIService.Delete(id);

            if (isDeleted)
                return Ok();
            else 
                return NotFound();
        }


        [HttpDelete]
        public IActionResult DeleteAll()
        {
             _orderIService.DeleteAll();
                return Ok();
        }
    }
}
