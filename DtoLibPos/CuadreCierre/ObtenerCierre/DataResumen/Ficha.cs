using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.ObtenerCierre.DataResumen
{
    public class Ficha
    {
        public List<PorDocumento> documentos { get; set; }
        public List<PorMetPago> metPago { get; set; }
        public Total total { get; set; }
    }
}