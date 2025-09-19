using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.Reportes.PagoResumen
{
    public class MetodoPago
    {
        public string codigoMP { get; set; }
        public string descMP { get; set; }
        public string codigoMoneda { get; set; }
        public string simboloMoneda { get; set; }
        public decimal tasaRespectoMonReferencia { get; set; }
        public decimal recibido { get; set; }
        public decimal montoRecibidoMonLocal { get; set; }
        public int cntMov { get; set; }
        public decimal tasaReferencia { get; set; }
    }
}
