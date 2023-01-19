using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.POS.MovCaja
{
    public class Ficha
    {
        public List<FichaMov> mov { get; set; }
        public List<FichaDet> det { get; set; }
    }
}