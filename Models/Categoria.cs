namespace ApiFinanzas.Models {
    public enum TipoCategoria {
        Ingreso,
        Gasto
    }
    public class Categoria {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoCategoria Tipo { get; set; }
        public decimal? Presupuesto { get; set; }
        public List<Ingreso> Ingresos { get; set; }
        public List<Gasto> Gastos { get; set; }

        public Categoria()
        {
            Ingresos = new List<Ingreso>();
            Gastos = new List<Gasto>();
        }
    }
}