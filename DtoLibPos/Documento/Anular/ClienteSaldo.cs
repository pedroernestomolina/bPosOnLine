using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Anular
{
    
    public class ClienteSaldo
    {

        public string autoCliente { get; set; }
        public decimal monto { get; set; }


        public ClienteSaldo() 
        {
            autoCliente = "";
            monto = 0m;
        }

    }

}