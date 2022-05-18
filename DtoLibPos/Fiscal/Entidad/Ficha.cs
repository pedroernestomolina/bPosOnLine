using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Fiscal.Entidad
{
    
    public class Ficha
    {

        public string id { get; set; }
        public string descripcion { get; set; }
        public decimal tasa { get; set; }
        public int codTasa { get; set; }


        public Ficha()
        {
            id = "";
            descripcion = "";
            tasa = 0.0m;
            codTasa = -1;
        }

    }

}