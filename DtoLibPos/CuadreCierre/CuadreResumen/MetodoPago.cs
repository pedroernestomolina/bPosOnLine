using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.CuadreResumen
{
    public class MetodoPago
    {
        public string idMedPago { get; set; }
        public string codMedPago { get; set; }
        public string descMedPago { get; set; }
        public string simboloMon { get; set; }
        public decimal ingreso { get; set; }
        public decimal? factor { get; set; }
        public decimal montoMonLocal { get; set; }
        public string codigoMon { get; set; }
    }
}