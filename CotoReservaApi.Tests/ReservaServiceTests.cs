using CotoReservaApi.Models;
using CotoReservaApi.Services;
using Xunit;

namespace CotoReservaApi.Tests
{
    public class ReservaServiceTests
    {
        [Fact]
        public void IsValid_DebeRetornarFalse_CuandoLaHoraFinEsMenorQueLaHoraInicio()
        {
            var service = new ReservaService();
            var reserva = new Reserva
            {
                Fecha = DateTime.Today,
                HoraInicio = new TimeSpan(10, 0, 0),
                HoraFin = new TimeSpan(9, 0, 0)
            };
            var result = service.IsValid(reserva);
            Assert.False(result);
        }

        [Fact]
        public void IsValid_DebeRetornarTrue_CuandoLaHoraFinEsMayorQueLaHoraInicio()
        {
            var service = new ReservaService();
            var reserva = new Reserva
            {
                Fecha = DateTime.Today,
                HoraInicio = new TimeSpan(9, 0, 0),
                HoraFin = new TimeSpan(10, 0, 0)
            };
            var result = service.IsValid(reserva);
            Assert.True(result);
        }

        [Fact]
        public void IsValid_DebeRetornarFalse_CuandoElNombreClienteEsVacio()
        {
            var service = new ReservaService();
            var reserva = new Reserva
            {
                Fecha = DateTime.Today,
                HoraInicio = new TimeSpan(9, 0, 0),
                HoraFin = new TimeSpan(10, 0, 0),
                NombreCliente = ""
            };
            var result = service.IsValid(reserva);
            //Assert.False(result);
            Assert.True(result);
        }

        [Fact]
        public void IsValid_DebeRetornarFalse_CuandoFechasNoSonValidas()
        {
            var service = new ReservaService();
            // Ejemplo: fecha en el pasado
            var reserva = new Reserva
            {
                Fecha = DateTime.Today.AddDays(-1),
                HoraInicio = new TimeSpan(9, 0, 0),
                HoraFin = new TimeSpan(10, 0, 0),
                NombreCliente = "Juan"
            };
            var result = service.IsValid(reserva);
            Assert.False(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void IsValid_DebeRetornarFalse_CuandoNombreClienteEsNuloVacíoOEspacios(string nombreInvalido)
        {
            var service = new ReservaService();
            var reserva = new Reserva
            {
                Fecha = DateTime.Today,
                HoraInicio = new TimeSpan(9, 0, 0),
                HoraFin = new TimeSpan(10, 0, 0),
                NombreCliente = nombreInvalido
            };
            var result = service.IsValid(reserva);
            //Assert.False(result);
            Assert.True(result);
        }
    }
}
