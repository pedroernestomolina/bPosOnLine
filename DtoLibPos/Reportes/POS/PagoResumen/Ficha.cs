using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.POS.PagoResumen
{
    
    public class Ficha
    {

        public decimal montoNCredito { get; set; }
        public decimal montoCambioDar { get; set; }
        public List<Detalle> detalle { get; set; }


        public Ficha() 
        {
            montoCambioDar = 0.0m;
            montoNCredito = 0.0m;
            detalle = new List<Detalle>();
        }

    }

}