using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.Factura
{
    
    public class Result
    {

        public int idVerificador { get; set; }
        public string autoDoc { get; set; }
        public string autoCierre { get; set; }
        public string numDoc { get; set; }
        public string codDoc { get; set; }
        public decimal montoDoc { get; set; }

        public Result()
        {
            idVerificador = -1;
            autoDoc = "";
            autoCierre = "";
            numDoc = "";
            codDoc = "";
            montoDoc = 0m;
        }

    }

}