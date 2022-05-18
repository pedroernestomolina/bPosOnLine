using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto.Existencia.Bloquear
{
    
    public class Ficha
    {

        public string autoPrd { get; set; }
        public string autoDeposito { get; set; }
        public decimal cantBloq { get; set; }


        public Ficha()
        {
            autoPrd = "";
            autoDeposito = "";
            cantBloq = 0.0m;
        }

    }

}