using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.POS.PagoResumen
{
    
    public class Detalle
    {

        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal importe { get; set; }
        public decimal montoRecibido { get; set; }
        public decimal tasa { get; set; }
        public string lote { get; set; }
        public string referencia { get; set; }
        public string medioCobroCodigo { get; set; }
        public string medioCobroDesc { get; set; }


        public Detalle() 
        {
            codigo = "";
            descripcion = "";
            importe = 0.0m;
            montoRecibido = 0.0m;
            tasa = 0.0m;
            lote = "";
            referencia = "";
            medioCobroCodigo = "";
            medioCobroDesc = "";
        }

    }

}