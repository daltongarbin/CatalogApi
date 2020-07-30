using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;
        private readonly CategoryRepository _categoryRepository;

        public ProductController(ProductRepository repository, CategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<ProductViewModel>>> Get()
        {
            var resposta = await _repository.Get();
            return Ok(resposta);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var resposta = await _repository.Get(id);
            return Ok(resposta);
        }

        [HttpPost("")]
        public async Task<ActionResult<Product>> Post([FromBody] EditorProductViewModel model)
        {
            var searchCategory = await _categoryRepository.Get(model.CategoryId);
            if (searchCategory == null)
            {
                return NotFound(new { message = "Categoria não encontrada" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var product = new Product();
                product.Title = model.Title;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.Image = model.Image;
                product.CreateDate = DateTime.Now; // Nunca recebe esta informação
                product.LastUpdateDate = DateTime.Now; // Nunca recebe esta informação
                product.CategoryId = model.CategoryId;

                await _repository.Save(product);
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível cadastrar o produto" });
            }
        }

        [HttpPost("v2/products")]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            var searchCategory = await _categoryRepository.Get(product.CategoryId);
            if (searchCategory == null)
            {
                return NotFound(new { message = "Categoria não encontrada" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _repository.Save(product);
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível cadastrar o produto" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<EditorProductViewModel>> Put(EditorProductViewModel model, int id)
        {
            if (model.Id != id)
            {
                return NotFound(new { message = "Produto não encontrado" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var product = await _repository.Get(id);
                product.Title = model.Title;
                product.CategoryId = model.CategoryId;
                // product.CreateDate = DateTime.Now; // Nunca altera a data de criação
                product.Description = model.Description;
                product.Image = model.Image;
                product.LastUpdateDate = DateTime.Now; // Nunca recebe esta informação
                product.Price = model.Price;
                product.Quantity = model.Quantity;

                await _repository.Update(product);
                return Ok(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este registro já foi atualizado" });

            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível editar o produto" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var searchProduct = await _repository.Get(id);
            if (searchProduct == null)
            {
                return BadRequest(new { message = "Não foi possível encontrar o produto" });
            }
            try
            {
                await _repository.Delete(searchProduct);
                return Ok(searchProduct);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível excluir o produto" });
            }
        }
    }
}