using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Entidad
{
    
    public class FichaMetodoPago
    {

        public string autoMedioPago { get; set; }
        public string codigoMedioPago { get; set; }
        public string descMedioPago { get; set; }
        public decimal montoRecibido { get; set; }
        public string lote { get; set; }
        public string referencia { get; set; }


        public FichaMetodoPago()
        {
            autoMedioPago = "";
            codigoMedioPago = "";
            descMedioPago = "";
            montoRecibido = 0.0m;
            lote = "";
            referencia = "";
        }

    }

}