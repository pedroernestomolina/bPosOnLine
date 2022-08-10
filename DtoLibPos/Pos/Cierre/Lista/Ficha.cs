using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pos.Cierre.Lista
{
    
    public class Ficha
    {

        public int id { get; set; }
        public string idEquipo { get; set; }
        public string hora  { get; set; }
        public  DateTime fecha{ get; set; }
        public  int cierreNro { get; set; }


        public Ficha() 
        {
            id = -1;
            idEquipo = "";
            hora = "";
            fecha = DateTime.Now.Date;
            cierreNro = -1;
        }

    }

}