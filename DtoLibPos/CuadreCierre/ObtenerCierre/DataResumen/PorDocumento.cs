using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.ObtenerCierre.DataResumen
{
    public class PorDocumento
    {
        public string descDoc { get; set; }
        public string codigoDoc { get; set; }
        public int signoDoc { get; set; }
        public int cntMovActivo { get; set; }
        public decimal importeMovActMonLocal { get; set; }
        public decimal importeMovActivoMonReferencia { get; set; }
        public int cntMovAnulado { get; set; }
        public decimal importMovAnuladoMonLocal { get; set; }
        public decimal importeMovAnuladoMonReferencia { get; set; }
        public int cntMovContado { get; set; }
        public decimal importeMovContadoMonLocal { get; set; }
        public decimal importeMovContadoMonReferencia { get; set; }
        public int cntMovCredito { get; set; }
        public decimal importeMovCreditoMonLocal { get; set; }
        public decimal importeMovCreditoMonReferencia { get; set; }
        public string siglasDoc { get; set; }
        public int cntTotalmov { get; set; }
        public string varianteDoc { get; set; }
    }
}