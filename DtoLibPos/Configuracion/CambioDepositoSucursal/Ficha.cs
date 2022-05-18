using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Configuracion.CambioDepositoSucursal
{
    
    public class Ficha
    {

        public string idSucursal { get; set; }
        public string idDeposito { get; set; }


        public Ficha()
        {
            idSucursal = "";
            idDeposito = "";
        }

    }

}
