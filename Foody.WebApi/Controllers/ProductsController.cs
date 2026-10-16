using AutoMapper;
using FluentValidation;
using Foody.WebApi.Context;
using Foody.WebApi.Dtos.ProductDtos;
using Foody.WebApi.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Foody.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IValidator<Product> _validator;
        AppDbContext _context;
        IMapper _mapper;

        public ProductsController(IValidator<Product> validator, AppDbContext context, IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return NotFound($"Id={id} olan ürün bulunamadı.");

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var validationResult = _validator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            var validationResult = _validator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var existing = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (existing == null)
                return NotFound($"Id={product.Id} olan ürün bulunamadı.");

            existing.ProductName = product.ProductName;
            existing.ProductDescription = product.ProductDescription;
            existing.Price = product.Price;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
                return NotFound($"Id={id} olan ürün bulunamadı.");

            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPost("CreateProductWithCategory")]
        public IActionResult CreateProductWithCategory(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        [HttpPost("ProductListWithCategory")]
        public IActionResult ProductListWithCategory( )
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            var productDtos = _mapper.Map<List<ResultProductWithCategoryDto>>(products);
            return Ok(productDtos);
        }

    }
} 