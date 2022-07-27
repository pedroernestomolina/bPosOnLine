using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.Factura
{
    
    public class FichaMedida
    {

        public string descMedida { get; set; }
        public decimal cnt { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }


        public FichaMedida() 
        {
            descMedida = "";
            cnt = 0;
            peso = 0m;
            volumen = 0m;
        }

    }

}