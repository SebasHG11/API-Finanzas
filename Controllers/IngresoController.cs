using ApiFinanzas.Models;
using ApiFinanzas.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanzas.Controllers {
    [Route("v1/[controller]")]
    public class IngresoController: ControllerBase {
        private readonly IIngresoService _ingresoService;

        public IngresoController(IIngresoService ingresoService)
        {
            _ingresoService = ingresoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngresos() {
            try{
                var Ingresos = await _ingresoService.MostrarIngresosAsync();
                return Ok(Ingresos);
            } catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngresoById(int id) {
            try{
                var ingreso = await _ingresoService.MostrarIngresoPorId(id);

                if(ingreso == null) {
                    return NotFound("Ingreso no encontrado");
                }

                return Ok(ingreso);
            }catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostIngreso([FromBody] IngresoDTO ingresoDTO) {
            if (ingresoDTO == null) {
                return BadRequest("Datos del ingreso no proporcionados.");
            }

            try{
                int nuevoIngresoId = await _ingresoService.CrearIngreso(ingresoDTO);
                return CreatedAtAction(nameof(GetIngresoById), new { id = nuevoIngresoId }, ingresoDTO);
            }catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngreso(int id,[FromBody] IngresoDTO ingresoDTO) {
            try{
                await _ingresoService.EditarIngreso(id, ingresoDTO);
                return NoContent();
            }catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngreso(int id) {
            try{
                await _ingresoService.EliminarIngreso(id);
                return NoContent();
            }catch(Exception ex) {
                return BadRequest(ex);
            }
        }
    }
}