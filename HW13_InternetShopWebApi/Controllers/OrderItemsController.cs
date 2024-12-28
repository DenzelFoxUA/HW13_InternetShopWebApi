using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW13_InternetShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IShopServices<OrderItem> _orderItemService;

        public OrderItemsController(IShopServices<OrderItem> orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<OrderItem> items = _orderItemService.GetAll();

            if (items is not null && items.Count() > 0)
                return Ok(items);
            else
                return NotFound();
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _orderItemService.GetById(id);

            if (order is not null)
            {
                return Ok(order);
            }

            return NotFound();

        }


        [HttpPost]
        public IActionResult Post([FromBody] OrderItem value)
        {
            if (value is not null)
            {

                return Ok(_orderItemService.Add(value));
            }
            else
                return BadRequest();
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderItem value)
        {
            if (value is not null)
            {
                return Ok(_orderItemService.Update(id, value));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _orderItemService.Delete(id);

            if (isDeleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
