using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.POS.VentCredito
{
    public class Ficha
    {
        public string docNumero { get; set; }
        public DateTime docEmision { get; set; }
        public string clienteNombre { get; set; }
        public string clienteCiRif { get; set; }
        public string clienteDir { get; set; }
        public string clienteTelf { get; set; }
        public decimal docImporteMonAct { get; set; }
        public decimal docImporteMonDiv { get; set; }
        public decimal docSaldoPendMonDiv { get; set; }
        public decimal montoBonoDiv { get; set; }
        public decimal porctBonoDiv { get; set; }
        public decimal factorCambio { get; set; }
    }
}