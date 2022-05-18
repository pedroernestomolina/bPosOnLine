using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.ProductoAdm.Lista
{
    
    public class Filtro
    {

        public string Cadena { get; set; }
        public string AutoDeposito { get; set; }
        public Enumerados.EnumMetodoBusqueda MetodoBusqueda { get; set; }


        public Filtro()
        {
            Cadena = "";
            AutoDeposito = "";
            MetodoBusqueda = Enumerados.EnumMetodoBusqueda.SinDefinir;
        }

    }

}