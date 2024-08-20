namespace ApiFinanzas.Models {
    public class IngresoDTO {
        public string? Concepto { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? Fecha { get; set; }
        public int? CategoriaId { get; set; }
    }
}