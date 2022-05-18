using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Sistema.TipoDocumento.Entidad
{
    
    public class Ficha
    {

        public string autoId { get; set; }
        public string tipo { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int signo { get; set; }
        public string siglas { get; set; }


        public Ficha()
        {
            autoId = "";
            tipo = "";
            codigo = "";
            nombre = "";
            siglas = "";
            signo = 1;
        }

    }

}