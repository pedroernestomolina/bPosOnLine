using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Configuracion.BusquedaCliente.Entidad
{

    public class Ficha
    {

        public string Usuario { get; set; }
        public Enumerados.EnumPreferenciaBusqueda ModoBusqueda
        {
            get
            {
                var modo = Enumerados.EnumPreferenciaBusqueda.SnDefinir;
                switch (Usuario.Trim().ToUpper())
                {
                    case "CODIGO":
                        modo = Enumerados.EnumPreferenciaBusqueda.Codigo;
                        break;
                    case "NOMBRE":
                        modo = Enumerados.EnumPreferenciaBusqueda.Nombre;
                        break;
                    case "CI/RIF":
                        modo = Enumerados.EnumPreferenciaBusqueda.CiRif;
                        break;
                }
                return modo;
            }
        }


        public Ficha()
        {
            Usuario = "";
        }

    }

}