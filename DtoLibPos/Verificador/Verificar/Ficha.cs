using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Verificador.Verificar
{
    
    public class Ficha
    {

        public int id { get; set; }
        public string usuarioCodVer { get; set; }
        public string usuarioNombreVer { get; set; }
        public string estatusVer { get; set; }


        public Ficha()
        {
            id = -1;
            usuarioCodVer = "";
            usuarioNombreVer = "";
            estatusVer = "";
        }

    }

}