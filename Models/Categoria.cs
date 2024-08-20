using System.Text.Json.Serialization;

namespace ApiFinanzas.Models {
    public class Categoria {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; } // Gasto o Ingreso
        public decimal? Presupuesto { get; set; }
        [JsonIgnore]
        public List<Ingreso> Ingresos { get; set; }
        [JsonIgnore]
        public List<Gasto> Gastos { get; set; }

        public Categoria()
        {
            Ingresos = new List<Ingreso>();
            Gastos = new List<Gasto>();
        }
    }
}