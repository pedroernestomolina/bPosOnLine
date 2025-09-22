using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.CuadreResumen
{
    public class Totales
    {
        public int cntDoc { get; set; }
        public decimal? montoMonLocal { get; set; }
        public decimal? montoMonReferencia { get; set; }
        public decimal? montoPendMonReferencia { get; set; }
        public decimal? cambioVueltoMonLocal { get; set; }
        public decimal? cambioVueltoMonReferencia { get; set; }
        public decimal? montoRecibidoMonLocal { get; set; }
        public decimal? montoRecibidoMonReferencia { get; set; }
        public decimal? vueltoDadoEfectivo { get; set; }
        public decimal? vueltoDadoDivisaMonLocal { get; set; }
        public decimal? vueltoPagoMovil { get; set; }
        public int? cntDivisaEntregada { get; set; }
        public decimal? bonoPagoDivisaMonLocal { get; set; }
        public decimal? bonoPagoDivisaMonReferencia { get; set; }
        public decimal? igtfMonLocal { get; set; }
    }
}
