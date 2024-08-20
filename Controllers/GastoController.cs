using ApiFinanzas.Models;
using ApiFinanzas.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanzas.Controllers {
    [Route("v1/[controller]")]
    public class GastoController: ControllerBase {
        private readonly IGastoService _gastoService;

        public GastoController(IGastoService gastoService)
        {
            _gastoService = gastoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGastos() {
            try{
                await _gastoService.MostrarGastos();
                return Ok();   
            } catch(Exception ex){
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGastoById(int id) {
            try{
                var Gasto = await _gastoService.MostrarGastoPorId(id);

                if(Gasto == null) {
                    throw new KeyNotFoundException("El gasto no existe.");
                }

                return Ok(Gasto);
            } catch(Exception ex){
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostGasto([FromBody] GastoDTO gastoDTO) {
            try{
                var nuevoGastoId = await _gastoService.CrearGasto(gastoDTO);
                return CreatedAtAction(nameof(GetGastoById), new { id = nuevoGastoId }, gastoDTO);
            } catch(Exception ex){
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasto(int id,[FromBody] GastoDTO gastoDTO) {
            try{
                await _gastoService.EditarGasto(id, gastoDTO);
                return NoContent();
            } catch(Exception ex){
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id) {
            try{
                await _gastoService.EliminarGasto(id);
                return NoContent();
            } catch(Exception ex){
                return BadRequest(ex);
            }
        }
    }
}