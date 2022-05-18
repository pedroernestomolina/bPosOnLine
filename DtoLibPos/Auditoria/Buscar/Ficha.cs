using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Auditoria.Buscar
{
    
    public class Ficha
    {

        public string autoDocumento { get; set; }
        public string autoTipoDocumento { get; set; }


        public Ficha()
        {
            autoDocumento = "";
            autoTipoDocumento = "";
        }

    }

}