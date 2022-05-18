using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Auditoria.Entidad
{
    
    public class Ficha
    {

        public string usuNombre { get; set; }
        public string usuCodigo { get; set; }
        public string usuAuto { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string motivo { get; set; }
        public string estacionEquipo { get; set; }


        public Ficha()
        {
            usuNombre = "";
            usuCodigo = "";
            usuAuto = "";
            fecha = DateTime.Now.Date;
            hora = "";
            motivo = "";
            estacionEquipo = "";
        }

    }

}