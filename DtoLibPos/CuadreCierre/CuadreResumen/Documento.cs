using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.CuadreResumen
{
    public class Documento
    {
        public int cntDoc { get; set; }
        public decimal montoMonLocal { get; set; }
        public decimal montoMonReferencia { get; set; }
        public decimal montoRecibidoMonLocal { get; set; }
        public decimal montoRecibidoMonReferencia { get; set; }
        public decimal cambioVueltoMonLocal { get; set; }
        public decimal cambioVueltoMonReferencia { get; set; }
        public string codigoDoc { get; set; }
        public string varianteDoc { get; set; }
        public string esCredito { get; set; }
        public string anulado { get; set; }
    }
}
