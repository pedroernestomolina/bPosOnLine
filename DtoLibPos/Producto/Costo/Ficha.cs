using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto.Costo
{
    public class Ficha
    {
        public string idPrd { get; set; }
        public string descPrd { get; set; }
        public int contEmpCompra { get; set; }
        public decimal costoEmpCompraMonLocal { get; set; }
        public decimal costoUndCompraMonLocal { get; set; }
        public decimal costoEmpCompraDivisa { get; set; }
    }
}