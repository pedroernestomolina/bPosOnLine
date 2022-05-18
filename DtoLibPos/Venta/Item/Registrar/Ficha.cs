using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Venta.Item.Registrar
{

    public class Ficha
    {


        public FichaItem item { get; set; }
        public FichaDeposito deposito { get; set; }
        public bool validarExistencia { get; set; }


        public Ficha()
        {
            validarExistencia = false;
            item = new FichaItem();
            deposito = new FichaDeposito();
        }

    }

}