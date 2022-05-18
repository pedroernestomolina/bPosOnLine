using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pendiente.Lista
{
    
    public class Ficha
    {

        public int id { get; set; }
        public string idCliente { get; set; }
        public string nombreCliente { get; set; }
        public string cirifCliente { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public int renglones { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }


        public Ficha()
        {
            id = -1;
            idCliente = "";
            nombreCliente = "";
            cirifCliente = "";
            monto = 0.0m;
            montoDivisa = 0.0m;
            renglones = 0;
            fecha = DateTime.Now.Date;
            hora = "";
        }

    }

}