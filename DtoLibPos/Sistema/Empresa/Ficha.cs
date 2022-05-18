using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Sistema.Empresa
{
    
    public class Ficha
    {

        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string CiRif { get; set; }
        public string Telefono { get; set; }


        public Ficha()
        {
            Nombre = "";
            Direccion = "";
            CiRif = "";
            Telefono = "";
        }

    }

}