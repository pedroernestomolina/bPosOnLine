using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pos.Cerrar
{
    
    public class Ficha
    {

        public int idOperador { get; set; }
        public string estatus { get; set; }
        public Arqueo arqueoCerrar { get; set; }


        public Ficha()
        {
            idOperador = -1;
            estatus = "";
            arqueoCerrar = new Arqueo();
        }

    }

}