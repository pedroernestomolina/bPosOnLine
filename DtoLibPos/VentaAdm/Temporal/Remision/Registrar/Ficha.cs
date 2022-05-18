using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.VentaAdm.Temporal.Remision.Registrar
{
    
    public class Ficha
    {

        public int idTemporal { get; set; }
        public string autoDoc { get; set; }
        public string numeroDoc { get; set; }
        public string codigoDoc { get; set; }
        public string nombreDoc { get; set; }
        public DateTime fechaDoc { get; set; }
        public decimal monto { get; set; }
        public decimal montoDivisa { get; set; }
        public int renglones { get; set; }
        public List<Item.Registrar.ItemDetalle> item { get; set; }


        public Ficha() 
        {
            idTemporal = -1;
            autoDoc = "";
            numeroDoc= "";
            codigoDoc = "";
            nombreDoc = "";
            fechaDoc = DateTime.Now.Date;
            monto = 0m;
            montoDivisa = 0m;
            renglones = 0;
            item = new List<Item.Registrar.ItemDetalle>();
        }

    }

}