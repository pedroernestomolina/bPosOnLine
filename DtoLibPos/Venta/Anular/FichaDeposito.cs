using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Venta.Anular
{
    
    public class FichaDeposito
    {

        public string autoProducto { get; set; }
        public string autoDeposito { get; set; }
        public decimal cantUndBloq { get; set; }


        public FichaDeposito()
        {
            autoProducto = "";
            autoDeposito = "";
            cantUndBloq = 0.0m;
        }

    }

}