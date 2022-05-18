using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Venta.Anular
{
    
    public class Ficha
    {

        public List<FichaItem> items { get; set; }
        public List<FichaDeposito> itemDeposito { get; set; }


        public Ficha()
        {
            items = new List<FichaItem>();
            itemDeposito = new List<FichaDeposito>();
        }

    }

}