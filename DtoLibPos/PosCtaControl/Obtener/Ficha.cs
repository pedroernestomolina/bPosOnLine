using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibPos.PosCtaControl.Obtener
{
    public class Ficha
    {
        public int idControl { get; set; }
        public string idCliente { get; set; }
        public decimal tasapos { get; set; }
        public string estatusProtegida { get; set; }
    }
}
