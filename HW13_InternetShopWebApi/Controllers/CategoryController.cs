using HW13_InternetShop._Contract.QueryModels;
using HW13_InternetShop.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

using QueryModel = HW13_InternetShop._Contract;
using DataModel = HW13_InternetShop.Data.Models;

namespace HW13_InternetShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IShopServices<Category> _categoryService;

        public CategoryController(IShopServices<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allItems = _categoryService.GetAll();
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
            var obj = _categoryService.GetById(id);
            if (obj is not null)
            {
                return Ok(obj);
            }
            else
                return NotFound();
            
        }


        [HttpPost]
        public IActionResult Post([FromBody] Category value)
        {
            if (value is not null)
            {
                return Ok(_categoryService.Add(value));
            }
            else
                return BadRequest();
            
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category value)
        {
            
            if (value != null)
            {
                return Ok(_categoryService.Update(id, value));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _categoryService.Delete(id);
            if (isDeleted)
            {
                return Ok();
            }
            else
                return NotFound();
            
        }
    }
}

