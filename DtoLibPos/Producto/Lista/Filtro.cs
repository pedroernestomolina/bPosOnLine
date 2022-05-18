using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto.Lista
{
    
    public class Filtro
    {

        public string Cadena { get; set; }
        public string AutoDeposito { get; set; }
        public string IdPrecioManejar { get; set; }
        public bool IsPorPlu { get; set; }


        public Filtro()
        {
            Cadena = "";
            AutoDeposito = "";
            IdPrecioManejar = "";
            IsPorPlu = false;
        }

    }

}