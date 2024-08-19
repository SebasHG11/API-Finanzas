namespace ApiFinanzas.Models {
    public class Gasto {
        public int Id { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}