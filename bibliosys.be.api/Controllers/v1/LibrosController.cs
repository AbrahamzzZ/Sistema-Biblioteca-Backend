using bibliosys.be.domain.Entity;
using bibliosys.be.domain.Repositories;
using bibliosys.be.infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bibliosys.be.api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroRepository _libroRepository;

        public LibrosController(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public class LibroRequest
        {
            public string Titulo { get; set; } = string.Empty;
            public string Autor { get; set; } = string.Empty;
            public string? Editorial { get; set; }
            public int? AnioPublicacion { get; set; }
            public string? Genero { get; set; }
            public int Stock { get; set; }
            public string? Ubicacion { get; set; }
        }

        private string GenerarCodigo()
        {
            return $"LIB-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var libros = await _libroRepository.GetAllAsync();
            return Ok(libros);
        }

        [HttpGet("activos")]
        public async Task<IActionResult> GetAllActivos()
        {
            var usuarios = await _libroRepository.GetAllActivosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var libro = await _libroRepository.GetByIdAsync(id);
            if (libro == null || !libro.Estado)
            {
                return NotFound();
            }

            return Ok(libro);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LibroRequest request)
        {
            var libro = new Libro
            {
                Codigo = GenerarCodigo(),
                Titulo = request.Titulo,
                Autor = request.Autor,
                Editorial = request.Editorial,
                Anio_Publicacion = request.AnioPublicacion,
                Genero = request.Genero,
                Stock = request.Stock,
                Ubicacion = request.Ubicacion,
                Estado = true
            };

            await _libroRepository.AddAsync(libro);
            return CreatedAtAction(nameof(GetById), new { id = libro.Id }, libro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LibroRequest request)
        {
            var libro = await _libroRepository.GetByIdAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            libro.Titulo = request.Titulo;
            libro.Autor = request.Autor;
            libro.Editorial = request.Editorial;
            libro.Anio_Publicacion = request.AnioPublicacion;
            libro.Genero = request.Genero;
            libro.Stock = request.Stock;
            libro.Ubicacion = request.Ubicacion;

            await _libroRepository.UpdateAsync(libro);
            return NoContent();
        }

        [HttpPatch("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var libro = await _libroRepository.GetByIdAsync(id);
            if (libro == null)
                return NotFound();

            libro.Estado = false;
            await _libroRepository.UpdateAsync(libro);

            return NoContent();
        }

        [HttpPatch("{id}/activar")]
        public async Task<IActionResult> Activar(int id)
        {
            var libro = await _libroRepository.GetByIdAsync(id);
            if (libro == null)
                return NotFound();

            libro.Estado = true;
            await _libroRepository.UpdateAsync(libro);

            return NoContent();
        }
    }
}

