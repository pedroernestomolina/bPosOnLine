using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Venta.Item.Zufu.ActualizarPrecio.Actualizar
{
    public class Data
    {
        public int idItem { get; set; }
        public decimal pNetoMonAct { get; set; }
        public decimal pFullMonDiv  { get; set; }
        public string aplicarPorcAumento { get; set; }
    }
}