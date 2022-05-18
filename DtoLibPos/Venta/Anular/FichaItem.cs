using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Venta.Anular
{
    
    public class FichaItem
    {

        public int idOperador { get; set; }
        public int idItem { get; set; }


        public FichaItem()
        {
            idOperador = -1;
            idItem = -1;
        }

    }

}