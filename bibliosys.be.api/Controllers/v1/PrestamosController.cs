using System;
using System.Threading.Tasks;
using bibliosys.be.application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bibliosys.be.api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamosController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        public class PrestamoCreateRequest
        {
            public int UsuarioId { get; set; }
            public int LibroId { get; set; }
            public DateTime FechaPrestamo { get; set; }
            public DateTime FechaLimite { get; set; }
        }

        public class PrestamoDevolucionRequest
        {
            public DateTime FechaDevolucion { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> CrearPrestamo([FromBody] PrestamoCreateRequest request)
        {
            try
            {
                var prestamo = await _prestamoService.CrearPrestamoAsync(
                    request.UsuarioId,
                    request.LibroId,
                    request.FechaPrestamo,
                    request.FechaLimite);

                return Ok(prestamo);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/devolver")]
        public async Task<IActionResult> RegistrarDevolucion(int id, [FromBody] PrestamoDevolucionRequest request)
        {
            try
            {
                await _prestamoService.RegistrarDevolucionAsync(id, request.FechaDevolucion);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/anular")]
        public async Task<IActionResult> Anular(int id, [FromBody] string? observacion = null)
        {
            try
            {
                await _prestamoService.AnularPrestamoAsync(id, observacion);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

