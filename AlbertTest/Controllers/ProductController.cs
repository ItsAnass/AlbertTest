using System;
using System.Threading.Tasks;
using Albert.BackendChallenge.Contracts;
using Albert.BackendChallenge.Entities;
using Albert.BackendChallenge.Repository;
using Albert.BackendChallenge.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Albert.BackendChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;


        public ProductController(IProductRepository productRepository )
        {
            _productRepository = productRepository;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {           

            return Ok(await _productRepository.GetProductById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {

            return Ok(await _productRepository.CreatProduct(product));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productRepository.GetAllProducts();

            return Ok(product);
        }
    }

}
