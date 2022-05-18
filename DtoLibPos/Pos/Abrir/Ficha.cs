using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pos.Abrir
{
    
    public class Ficha
    {

        public string codSucursal { get;set; }
        public string idEquipo { get; set; }
        public Operador operadorAbrir { get; set; }
        public Arqueo arqueoAbrir { get; set; }
        public Resumen resumenAbrir { get; set; }


        public Ficha()
        {
            codSucursal="";
            idEquipo = "";
            operadorAbrir = new Operador();
            arqueoAbrir = new Arqueo();
            resumenAbrir = new Resumen();
        }

    }

}