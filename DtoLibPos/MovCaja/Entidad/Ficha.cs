using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.MovCaja.Entidad
{
    public class Ficha
    {
        public int IdMov { get; set; }
        public int IdOperador { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaMov { get; set; }
        public decimal MontoMov { get; set; }
        public decimal MontoDivisaMov { get; set; }
        public decimal FactorCambio { get; set; }
        public string ConceptoMov { get; set; }
        public string DetalleMov { get; set; }
        public string EstatusAnulado { get; set; }
        public string TipoMov { get; set; }
        public int SignoMov { get; set; }
        public string NumeroMov { get; set; }
        public List<Detalle> Detalles { get; set; }
    }
}