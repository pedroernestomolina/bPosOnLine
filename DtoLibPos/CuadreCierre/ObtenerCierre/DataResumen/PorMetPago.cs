using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.ObtenerCierre.DataResumen
{
    public class PorMetPago
    {
        public string codigoMP { get; set; }
        public string descMP { get; set; }
        public string codigoMon { get; set; }
        public string descMon { get; set; }
        public string simboloMon { get; set; }
        public decimal tasaFactorPonderadoMon { get; set; }
        public decimal montoSegunSistema { get; set; }
        public decimal montoSegunUsuario { get; set; }
        public decimal importeMonLocal { get; set; }
    }
}