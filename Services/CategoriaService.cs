using ApiFinanzas.Data;
using ApiFinanzas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanzas.Services {
    public class CategoriaService : ICategoriaService
    {
        private readonly ApiContext _context;

        public CategoriaService(ApiContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Categoria>> MostrarCategorias() {
            var Categorias = await _context.Categorias.ToListAsync();
            return Categorias;
        }

        public async Task<Categoria> MostrarCategoriaPorId(int id) {
            var categoriaActual = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if(categoriaActual == null) {
                throw new KeyNotFoundException("La categoria no existe");
            }

            return categoriaActual;
        }

        public async Task<int> CrearCategoriaGastos(CategoriaDTO categoriaDTO) {
            var nuevaCategoria = new Categoria {
                Nombre = categoriaDTO.Nombre ?? throw new ArgumentNullException("El nombre de la categoria no puede ser nula"),
                Tipo = "Gastos",
                Presupuesto = categoriaDTO.Presupuesto
            };

            await _context.Categorias.AddAsync(nuevaCategoria);
            await _context.SaveChangesAsync();

            return nuevaCategoria.Id;
        }

        public async Task<int> CrearCategoriaIngresos(CategoriaDTO categoriaDTO) {
            var nuevaCategoria = new Categoria {
                Nombre = categoriaDTO.Nombre ?? throw new ArgumentNullException("El nombre de la categoria no puede ser nula"),
                Tipo = "Ingresos",
                Presupuesto = categoriaDTO.Presupuesto
            };

            await _context.Categorias.AddAsync(nuevaCategoria);
            await _context.SaveChangesAsync();

            return nuevaCategoria.Id;
        }

        public async Task EditarCategoria(int id, CategoriaDTO categoriaDTO) {
            var categoriaActual = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if(categoriaActual == null) {
                throw new KeyNotFoundException("La categoria no existe");
            }

            categoriaActual.Nombre = categoriaDTO.Nombre ?? categoriaActual.Nombre;
            categoriaActual.Presupuesto = categoriaDTO.Presupuesto ?? categoriaActual.Presupuesto;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarCategoria(int id) {
            var categoriaActual = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if(categoriaActual == null) {
                throw new KeyNotFoundException("La categoria no existe");
            }

            _context.Categorias.Remove(categoriaActual);
            await _context.SaveChangesAsync();
        }
    }

    public interface ICategoriaService {
        Task<ICollection<Categoria>> MostrarCategorias();
        Task<Categoria> MostrarCategoriaPorId(int id);
        Task<int> CrearCategoriaGastos(CategoriaDTO categoriaDTO);
        Task<int> CrearCategoriaIngresos(CategoriaDTO categoriaDTO);
        Task EditarCategoria(int id, CategoriaDTO categoriaDTO);
        Task EliminarCategoria(int id);
    }
}