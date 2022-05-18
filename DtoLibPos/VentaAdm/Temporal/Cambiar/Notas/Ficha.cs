using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Cambiar.Notas
{
    
    public class Ficha
    {

        public int id { get; set; }
        public string notas { get; set; }


        public Ficha()
        {
            id = -1;
            notas = "";
        }

    }

}