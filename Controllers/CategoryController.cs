using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;
using ProductCatalog.Repositories;

namespace ProductCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;

        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<List<Category>>> Get()
        {
            var categories = await _repository.Get();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var categories = await _repository.Get(id);
            return Ok(categories);
        }

        [HttpGet("{id:int}/products")]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<List<Product>>> GetProducts(int id)
        {
            var categories = await _repository.GetProducts(id);
            return Ok(categories);
        }

        [HttpPost("")]
        public async Task<ActionResult<Category>> Post([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _repository.Save(category);
                return Ok(category);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar a categoria" });

            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> Put([FromBody] Category category, int id)
        {
            if (id != category.Id)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _repository.Update(category);
                return Ok(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este registro já foi atualizado" });

            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível atualizar a categoria" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var searchCategory = await _repository.Get(id);
            if (searchCategory == null)
            {
                return NotFound(new { message = "Categoria não encontrada" });
            }
            try
            {
                await _repository.Exclude(searchCategory);
                return Ok(searchCategory);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover a categoria" });

            }
        }
    }
}