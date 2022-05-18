using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.ProductoAdm.ListaResumen
{
    
    public class Filtro
    {

        public string Cadena { get; set; }
        public Enumerados.EnumMetodoBusqueda MetodoBusqueda { get; set; }


        public Filtro()
        {
            Cadena = "";
            MetodoBusqueda = Enumerados.EnumMetodoBusqueda.SinDefinir;
        }

    }

}