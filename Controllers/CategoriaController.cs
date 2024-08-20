using ApiFinanzas.Models;
using ApiFinanzas.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanzas.Controllers {
    [Route("v1/[controller]")]
    public class CategoriaController: ControllerBase {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategorias() {
            try{
                var Categorias = await _categoriaService.MostrarCategorias();
                return Ok(Categorias);
            } catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriaById(int id) {
            try{
                var Categoria = await _categoriaService.MostrarCategoriaPorId(id);

                if(Categoria == null) {
                    return NotFound();
                }

                return Ok(Categoria);
            } catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPost("Gastos")]
        public async Task<IActionResult> PostCategoriaGastos([FromBody] CategoriaDTO categoriaDTO) {
            if (categoriaDTO == null) {
                return BadRequest("Datos de la categoria no proporcionados.");
            }

            try{
                var nuevaCategoriaId = await _categoriaService.CrearCategoriaGastos(categoriaDTO);
                return CreatedAtAction(nameof(GetCategoriaById), new { id = nuevaCategoriaId }, categoriaDTO);
            } catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPost("Ingresos")]
        public async Task<IActionResult> PostCategoriaIngresos([FromBody] CategoriaDTO categoriaDTO) {
            if (categoriaDTO == null) {
                return BadRequest("Datos de la categoria no proporcionados.");
            }

            try{
                var nuevaCategoriaId = await _categoriaService.CrearCategoriaIngresos(categoriaDTO);
                return CreatedAtAction(nameof(GetCategoriaById), new { id = nuevaCategoriaId }, categoriaDTO);
            } catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id,[FromBody] CategoriaDTO categoriaDTO) {
            try{
                await _categoriaService.EditarCategoria(id, categoriaDTO);
                return NoContent();
            } catch(Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id) {
            try{
                await _categoriaService.EliminarCategoria(id);
                return NoContent();
            } catch(Exception ex) {
                return BadRequest(ex);
            }
        }
    }
}