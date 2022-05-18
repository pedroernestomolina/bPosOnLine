using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pos.EnUso
{
    
    public class Ficha
    {

        public int id { get; set; }
        public string idArqueoCierre { get; set; }
        public int idResumen { get; set; }
        public string idUsuario { get; set; }
        public string codUsuario { get; set; }
        public string nomUsuario { get; set; }
        public DateTime fechaApertura { get; set; }
        public string horaApertura { get; set; }


        public Ficha()
        {
            id = -1;
            idArqueoCierre = "";
            idResumen = -1;
            idUsuario = "";
            codUsuario = "";
            nomUsuario = "";
            fechaApertura = DateTime.Now.Date;
            horaApertura = "";
        }

    }

}