using System;
using System.Linq;
using System.Threading.Tasks;
using Albert.BackendChallenge.Contracts;
using Albert.BackendChallenge.Entities;
using Albert.BackendChallenge.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Albert.BackendChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IReservationRepository  _reservation;


        public OrdersController(IProductRepository productRepository, IReservationRepository reservation)
        {
            _productRepository = productRepository;
            _reservation = reservation;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderAPIRequestItem request)
        {
            
            var product = await _productRepository.GetProductById(request.Id);

            var check = await _reservation.CheckQuantity(product, request.Amount);
           
            if (check == false)
            {
                await _productRepository.RemoveItemsFromStock(product.Id, request.Amount);
             
            }
            return Ok(await _productRepository.GetProductById(product.Id));
        }


        //[HttpPost]
        //public async Task<IActionResult> Add([FromBody] OrderAPIRequestItem request)
        //{

        //    var product = await _productRepository.GetProductById(request.Id);
        //    await _productRepository.AddItemsToStock(product.Id, request.Amount);

        //    return Ok(await _productRepository.GetProductById(product.Id));

        //}
    }
}
