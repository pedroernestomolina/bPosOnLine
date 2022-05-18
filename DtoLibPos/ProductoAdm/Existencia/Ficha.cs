using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.ProductoAdm.Existencia
{
    
    public class Ficha
    {

        public decimal real { get; set; }
        public decimal disponible { get; set; }


        public Ficha() 
        {
            real = 0m;
            disponible = 0m;
        }

    }

}