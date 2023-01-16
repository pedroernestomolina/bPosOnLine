using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.MovCaja.Anular
{
    public class Ficha
    {
        public int IdOperador { get; set; }
        public int IdMovimiento { get; set; }
        public string Motivo { get; set; }
        public string AutoUsuAut { get; set; }
        public string CodigoUsuAut { get; set; }
        public string NombreUsuAut { get; set; }
    }
}