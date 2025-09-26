using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Entidad
{
    public class FichaMetodoPago
    {
        public string codigoMP { get; set; }
        public string descMP { get; set; }
        public decimal montoIngresado{ get; set; }
        public string lote { get; set; }
        public string referencia { get; set; }
        public string codigoMon { get; set; }
        public string simboloMon { get; set; }
        public decimal tasaMon { get; set; }
        public decimal montoMonLocal { get; set; }
        public decimal tasaFactorRef { get; set; }
    }
}