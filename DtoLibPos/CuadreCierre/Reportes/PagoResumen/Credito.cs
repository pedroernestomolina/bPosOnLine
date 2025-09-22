using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.Reportes.PagoResumen
{
    public class Credito
    {
        public decimal? montoCreditoMonLocal { get; set; }
        public decimal? montoCreditoMonReferencia { get; set; }
        public int cntMovCredito { get; set; }
    }
}
