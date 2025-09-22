using CotoReservaApi.Models;
using CotoReservaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CotoReservaApi.Controllers
{
    [ApiController]
    [Route("api/reserva")]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservaController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Reserva nuevaReserva)
        {
            if (!_reservaService.IsValid(nuevaReserva))
            {
                return BadRequest(new { message = "La reserva no es válida o no hay salones disponibles en ese horario." });
            }

            var reservaCreada = _reservaService.Add(nuevaReserva);

            return CreatedAtAction(nameof(GetReservasByFecha), new { fecha = reservaCreada.Fecha.ToString("yyyy-MM-dd") }, reservaCreada);
        }

        [HttpGet]
        public IActionResult GetTodasLasReservas()
        {
            var reservas = _reservaService.GetTodasLasReservas();
            return Ok(reservas);
        }

        [HttpGet("{fecha?}")]
        public IActionResult GetReservasByFecha(DateTime? fecha)
        {
            if (fecha.HasValue)
            {
                var reservas = _reservaService.GetReservasByFecha(fecha.Value);
                return Ok(reservas);
            }
            else
            {
                var todasReservas = _reservaService.GetTodasLasReservas();
                return Ok(todasReservas);
            }
        }
    }
}
