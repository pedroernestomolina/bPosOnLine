using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.Clientes.Maestro
{
    
    public class Ficha
    {

        public string codigo { get; set; }
        public string nombre { get; set; }
        public string ciRif { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        public string celular { get; set; }
        public string dirFiscal { get; set; }
        public string estatus { get; set; }


        public Ficha()
        {
            codigo = "";
            nombre = "";
            ciRif = "";
            telefono1 = "";
            telefono2 = "";
            celular = "";
            dirFiscal = "";
            estatus = "";
        }

    }

}