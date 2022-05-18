using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Anular  
{

    public class Deposito
    {

        public string nombrePrd { get; set; }
        public string AutoProducto { get; set; }
        public string AutoDeposito { get; set; }
        public decimal CantUnd { get; set; }


        public Deposito()
        {
            nombrePrd = "";
            AutoProducto = "";
            AutoDeposito = "";
            CantUnd = 0.0m;
        }

    }

}