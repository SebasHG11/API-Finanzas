using ApiFinanzas.Data;
using ApiFinanzas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanzas.Services {
    public class IngresoService {
        private readonly ApiContext _context;

        public IngresoService(ApiContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Ingreso>> MostrarIngresosAsync() {
            var Ingresos = await _context.Ingresos.ToListAsync();
            return Ingresos;
        }

        public async Task<Ingreso> MostrarIngresoPorId(int id) {
            var ingresoActual = await _context.Ingresos.FirstAsync(i => i.Id == id);

            if(ingresoActual == null) {
                throw new KeyNotFoundException("El ingreso con el id ingresado no existe.");
            }

            return ingresoActual;
        }

        public async Task CrearIngreso(IngresoDTO ingresoDTO) {
            var ingresoNuevo = new Ingreso {
                Concepto = ingresoDTO.Concepto ?? throw new ArgumentNullException("El concepto no puede ser nulo"),
                Monto = ingresoDTO.Monto ?? throw new ArgumentNullException("El monto no puede ser nulo"),
                Fecha = ingresoDTO.Fecha ?? throw new ArgumentNullException("El fecha no puede ser nula"),
                CategoriaId = ingresoDTO.CategoriaId ?? throw new ArgumentNullException("El id de la categoria no puede ser nulo")
            };

            await _context.Ingresos.AddAsync(ingresoNuevo);
            await _context.SaveChangesAsync();
        }

        public async Task EditarIngreso(int id, IngresoDTO ingresoDTO) {
            var ingresoActual = await _context.Ingresos.FirstOrDefaultAsync(i => i.Id == id);

            if(ingresoActual == null) {
                throw new KeyNotFoundException("El ingreso con el id ingresado no existe.");
            }

            ingresoActual.Concepto = ingresoDTO.Concepto ?? ingresoActual.Concepto;
            ingresoActual.Monto = ingresoDTO.Monto ?? ingresoActual.Monto;
            ingresoActual.Fecha = ingresoDTO.Fecha ?? ingresoActual.Fecha;
            ingresoActual.CategoriaId = ingresoDTO.CategoriaId ?? ingresoActual.CategoriaId;

            await _context.SaveChangesAsync();
        }

        public async Task EliminarIngreso(int id) {
            var ingresoActual = await _context.Ingresos.FirstOrDefaultAsync(i => i.Id == id);

            if(ingresoActual == null) {
                throw new KeyNotFoundException("El ingreso con el id ingresado no existe.");
            }

            _context.Ingresos.Remove(ingresoActual);
            await _context.SaveChangesAsync();
        }
    }
}