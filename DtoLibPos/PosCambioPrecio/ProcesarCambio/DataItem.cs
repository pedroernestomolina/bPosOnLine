using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.PosCambioPrecio.ProcesarCambio
{
    public class DataItem
    {
        public int idItem { get; set; }
        public int idOperador { get; set; }
        public decimal pNetoMonAct { get; set; }
        public decimal pFullMonDiv  { get; set; }
        public string aplicarPorcAumento { get; set; }
    }
}