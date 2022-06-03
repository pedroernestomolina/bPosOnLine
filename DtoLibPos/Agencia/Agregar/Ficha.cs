using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Agencia.Agregar
{
    
    public class Ficha: BaseFicha
    {

        public string codSucursal { get; set; }


        public Ficha() 
        {
            codSucursal = "";
            nombre = "";
        }

    }

}