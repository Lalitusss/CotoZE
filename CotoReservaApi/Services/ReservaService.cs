using CotoReservaApi.Models;

namespace CotoReservaApi.Services
{
    public class ReservaService
    {
        private readonly List<Reserva> _reservas = new();
        private readonly List<Salon> _salones;

        public ReservaService()
        {
            // Se crean los 3 salones mínimos
            _salones = new List<Salon>
        {
            new Salon { Id = 1, Nombre = "Salon Principal", Capacidad = 50 },
            new Salon { Id = 2, Nombre = "Salon Festejo", Capacidad = 30 },
            new Salon { Id = 3, Nombre = "Salon Fiesta", Capacidad = 40 }
        };
        }

        public bool IsValid(Reserva nuevaReserva)
        {
            // 1. Validar que la hora de fin sea mayor que la de inicio.
            if (nuevaReserva.HoraFin <= nuevaReserva.HoraInicio)
            {
                return false;
            }

            // 2. Validar que la reserva esté en la franja horaria de 9:00 a 18:00 hs.
            var apertura = new TimeSpan(9, 0, 0);
            var cierre = new TimeSpan(18, 0, 0);
            if (nuevaReserva.HoraInicio < apertura || nuevaReserva.HoraFin > cierre)
            {
                return false;
            }

            // 3. Validar que no haya superposición con 30 minutos de acondicionamiento.
            var buffer = TimeSpan.FromMinutes(30);
            var reservasExistentes = _reservas.Where(r => r.Fecha.Date == nuevaReserva.Fecha.Date).ToList();

            foreach (var salon in _salones)
            {
                var reservasDelSalon = reservasExistentes.Where(r => r.SalonId == salon.Id).ToList();
                bool isSalonAvailable = !reservasDelSalon.Any(r =>
                    (nuevaReserva.HoraInicio < r.HoraFin + buffer && nuevaReserva.HoraFin + buffer > r.HoraInicio) ||
                    (r.HoraInicio < nuevaReserva.HoraFin + buffer && r.HoraFin + buffer > nuevaReserva.HoraInicio)
                );

                if (isSalonAvailable)
                {
                    nuevaReserva.SalonId = salon.Id;
                    return true;
                }
            }

            return false;
        }

        public Reserva Add(Reserva nuevaReserva)
        {
            nuevaReserva.Id = Guid.NewGuid();
            _reservas.Add(nuevaReserva);
            return nuevaReserva;
        }

        public IEnumerable<Reserva> GetTodasLasReservas()
        {
            return _reservas.ToList();
        }
        public IEnumerable<Reserva> GetReservasByFecha(DateTime fecha)
        {
            return _reservas.Where(r => r.Fecha.Date == fecha.Date).ToList();
        }
    }
}
