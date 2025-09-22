namespace CotoReservaApi.Models
{
    public class Reserva
    {
        public Guid Id { get; set; }
        public int SalonId { get; set; }
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
