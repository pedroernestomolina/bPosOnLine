using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.ProductoAdm.ListaResumen
{
    
    public class Ficha
    {

        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }


        public Ficha()
        {
            Id= "";
            Codigo = "";
            Nombre = "";
            Estatus = "";
        }

    }

}