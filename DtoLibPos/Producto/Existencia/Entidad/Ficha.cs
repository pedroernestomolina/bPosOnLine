using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto.Existencia.Entidad
{
    
    public class Ficha
    {

        public string autoPrd { get; set; }
        public string autoDeposito { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public string codigoDeposito { get; set; }
        public string nombreDeposito { get; set; }
        public decimal exFisica { get; set; }
        public decimal exDisponible { get; set; }


        public Ficha()
        {
            autoPrd = "";
            autoDeposito = "";
            codigoPrd = "";
            codigoDeposito = "";
            nombrePrd = "";
            nombreDeposito = "";
            exFisica = 0.0m;
            exDisponible = 0.0m;
        }

    }

}