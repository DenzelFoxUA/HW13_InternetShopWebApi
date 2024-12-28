
using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

using DataModel = HW13_InternetShop.Data.Models;

namespace HW13_InternetShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IShopServices<Brand> _brandService;

        public BrandController(IShopServices<Brand> brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allItems = _brandService.GetAll();
            if (allItems is not null && allItems.Count() > 0)
            {
                return Ok(allItems);
            }
            else
                return NotFound();
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var found = _brandService.GetById(id);
            if(found is not null)
            {
                return Ok(found);
            }
            else
            {
                return NotFound();
            }
            
        }


        [HttpPost]
        public IActionResult Post([FromBody] Brand value)
        {
            if (value is not null)
            {
                return Ok(_brandService.Add(value));
            }
            else
                return BadRequest();
            
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Brand value)
        {
            
            if (value is not null)
            {
                return Ok(_brandService.Update(id, value));
            }
            else
                return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var found = _brandService.Delete(id);

            if(found)
            {
                return Ok(found);
            }
            else
                return NotFound();
            
        }
    }
}
