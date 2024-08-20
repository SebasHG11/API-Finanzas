using ApiFinanzas.Data;
using ApiFinanzas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanzas.Services {
    public class GastoService: IGastoService {
        private readonly ApiContext _context;

        public GastoService(ApiContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Gasto>> MostrarGastos() {
            var Gastos = await _context.Gastos
            .Include(g => g.Categoria)
            .ToListAsync();

            return Gastos;
        }

        public async Task<Gasto> MostrarGastoPorId(int id) {
            var gastoActual = await _context.Gastos
            .Include(g => g.Categoria)
            .FirstOrDefaultAsync(g => g.Id == id);

            if(gastoActual == null) {
                throw new KeyNotFoundException("El gasto no existe.");
            }

            return gastoActual;
        }

        public async Task<int> CrearGasto(GastoDTO gastoDTO) {
            var gastoNuevo = new Gasto {
                Concepto = gastoDTO.Concepto ?? throw new ArgumentNullException("El concepto no puede ser nulo"),
                Monto = gastoDTO.Monto ?? throw new ArgumentNullException("El monto no puede ser nulo"),
                Fecha = gastoDTO.Fecha ?? throw new ArgumentNullException("La fecha no puede ser nula"),
                CategoriaId = gastoDTO.CategoriaId ?? throw new ArgumentNullException("El id de la categoria no puede ser nulo")
            };

            await _context.Gastos.AddAsync(gastoNuevo);
            await _context.SaveChangesAsync();

            return gastoNuevo.Id;
        }

        public async Task EditarGasto(int id, GastoDTO gastoDTO) {
            var gastoActual = await _context.Gastos.FirstOrDefaultAsync(g => g.Id == id);

            if(gastoActual == null) {
                throw new KeyNotFoundException("El gasto no existe");
            }

            gastoActual.Concepto = gastoDTO.Concepto ?? gastoActual.Concepto;
            gastoActual.Monto = gastoDTO.Monto ?? gastoActual.Monto;
            gastoActual.Fecha = gastoDTO.Fecha ?? gastoActual.Fecha;
            gastoActual.CategoriaId = gastoDTO.CategoriaId ?? gastoActual.CategoriaId;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarGasto(int id) {
            var gastoActual = await _context.Gastos.FirstOrDefaultAsync(g => g.Id == id);

            if(gastoActual == null) {
                throw new KeyNotFoundException("El gasto no existe");
            }

            _context.Gastos.Remove(gastoActual);
            await _context.SaveChangesAsync();
        }
    }

    public interface IGastoService {
        Task<ICollection<Gasto>> MostrarGastos();
        Task<Gasto> MostrarGastoPorId(int id);
        Task<int> CrearGasto(GastoDTO gastoDTO);
        Task EditarGasto(int id, GastoDTO gastoDTO);
        Task EliminarGasto(int id);
    }
}