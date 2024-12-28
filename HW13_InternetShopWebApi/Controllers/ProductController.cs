using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace HW13_InternetShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IShopServices<Product> _prodService;

        public ProductController(IShopServices<Product> prodService)
        {
            _prodService = prodService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _prodService.GetAll().ToList();
            if (products.Count() > 0)
                return Ok(products);
            else
                return NotFound();
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var found = _prodService.GetById(id);
            if(found is not null)
                return Ok(found);
            else
                return NotFound();
        }


        [HttpPost]
        public IActionResult Post([FromBody] Product value)
        {
            if (value is not null)
                return Ok(_prodService.Add(value));
            else
                return BadRequest();
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product value)
        {
            if (value is not null)
            {
                return Ok(_prodService.Update(id, value));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _prodService.Delete(id);
            if (isDeleted)
                return Ok();
            else
                return NotFound();
        }
    }
}
