using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.NotaCredito
{

    public class FichaDeposito
    {

        public string AutoProducto { get; set; }
        public string AutoDeposito { get; set; }
        public decimal CantUnd { get; set; }


        public FichaDeposito()
        {
            AutoProducto = "";
            AutoDeposito = "";
            CantUnd = 0.0m;
        }

    }

}