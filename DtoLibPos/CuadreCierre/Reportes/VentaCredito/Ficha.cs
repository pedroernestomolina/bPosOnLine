using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.Reportes.VentaCredito
{
    public class Ficha
    {
        public string nroDoc { get; set; }
        public DateTime fechaEmisionDoc { get; set; }
        public string entidadDoc { get; set; }
        public string ciRifDoc { get; set; }
        public decimal importeMonLocal { get; set; }
        public decimal importeMonReferencia { get; set; }
        public decimal montoPendCxcMonReferencia { get; set; }
        public decimal bonoPagoDivisaMonReferencia { get; set; }
        public int signoDoc { get; set; }
        public string siglasDoc { get; set; }
        public string estatusAnulado { get; set; }
        public string nroDocAplica { get; set; }
    }
}
