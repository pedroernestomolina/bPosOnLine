using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto_ModoAdm.Precio
{
    public class Precio
    {
        public int idPrecioVenta { get; set; }
        public int idTipoPrecio { get; set; }
        public int idTipoEmpVenta { get; set; }
        public string autoEmp { get; set; }
        public int contEmp { get; set; }
        public string descEmp { get; set; }
        public decimal utEmp { get; set; }
        public decimal pnEmp { get; set; }
        public decimal pfdEmp { get; set; }
        public string ofertaEstatus { get; set; }
        public DateTime ofertaDesde { get; set; }
        public DateTime ofertaHasta { get; set; }
        public decimal ofertaPorct { get; set; }
        public string tipoEmp { get; set; }
        public string decimales { get; set; }
    }
}