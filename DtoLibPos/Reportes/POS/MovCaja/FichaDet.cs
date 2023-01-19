using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.POS.MovCaja
{
    public class FichaDet
    {
        public string codigoMed { get; set; }
        public string descMed { get; set; }
        public decimal monto { get; set; }
        public int cntDivisa { get; set; }
        public string esDivisa { get; set; }
    }
}