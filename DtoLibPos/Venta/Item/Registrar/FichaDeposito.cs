using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Venta.Item.Registrar
{

    public class FichaDeposito
    {

        public string autoPrd { get; set; }
        public string autoDeposito { get; set; }
        public decimal cantBloq { get; set; }


        public FichaDeposito()
        {
            autoPrd = "";
            autoDeposito = "";
            cantBloq = 0.0m;
        }

    }

}