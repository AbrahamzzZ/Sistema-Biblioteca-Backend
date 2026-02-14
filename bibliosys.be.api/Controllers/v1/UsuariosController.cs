using System.Threading.Tasks;
using bibliosys.be.domain.Entity;
using bibliosys.be.domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bibliosys.be.api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public class UsuarioRequest
        {
            public string NombreCompleto { get; set; } = string.Empty;
            public string Cedula { get; set; } = string.Empty;
            public string CorreoElectronico { get; set; } = string.Empty;
            public string? Direccion { get; set; }
            public string? Telefono { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("activos")]
        public async Task<IActionResult> GetAllActivos()
        {
            var usuarios = await _usuarioRepository.GetAllActivosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById(int id) { 
            var usuario = await _usuarioRepository.GetByIdAsync(id); 
            if (usuario == null || !usuario.Estado) 
            { 
                return NotFound(); 
            } 
            return Ok(usuario);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioRequest request)
        {
            var usuario = new Usuario
            {
                Nombre_Completo = request.NombreCompleto,
                Cedula = request.Cedula,
                Correo_Electronico = request.CorreoElectronico,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                Estado = true
            };

            await _usuarioRepository.AddAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioRequest request)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Nombre_Completo = request.NombreCompleto;
            usuario.Cedula = request.Cedula;
            usuario.Correo_Electronico = request.CorreoElectronico;
            usuario.Direccion = request.Direccion;
            usuario.Telefono = request.Telefono;

            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }

        [HttpPatch("{id}/desactivar")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();

            if (!usuario.Estado)
                return NoContent();

            usuario.Estado = false;
            await _usuarioRepository.UpdateAsync(usuario);

            return NoContent();
        }

        [HttpPatch("{id}/activar")]
        public async Task<IActionResult> Activar(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();

            if (usuario.Estado)
                return NoContent();

            usuario.Estado = true;
            await _usuarioRepository.UpdateAsync(usuario);

            return NoContent();
        }
    }
}

