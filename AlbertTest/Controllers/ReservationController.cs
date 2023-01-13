using System;
using System.Threading.Tasks;
using Albert.BackendChallenge.Contracts;
using Albert.BackendChallenge.Entities;
using Albert.BackendChallenge.Repository;
using Albert.BackendChallenge.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Albert.BackendChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {

        private readonly IReservationRepository _reservationRepository;


        public ReservationController(IReservationRepository  reservationRepository )
        {
            _reservationRepository = reservationRepository;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {           

            return Ok(await _reservationRepository.GetReservationById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reservation reservation)
        {

            return Ok(await _reservationRepository.CreatReservation(reservation));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var product = await _reservationRepository.GetAllReservations();

            return Ok(product);
        }
    }

}
