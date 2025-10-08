using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.PosCambioPrecio.ObtenerDataItem
{
    public class Ficha
    {
        public int idItem { get; set; }
        public int idOperador { get; set; }
        public string codigoPrd { get; set; }
        public string descPrd { get; set; }
        public decimal pNetoMonLocal { get; set; }
        public decimal pFullMonReferencia { get; set; }
        public decimal tasaIva { get; set; }
        public decimal costoUnd { get; set; }
        public int contEmpqVta { get; set; }
        public string descEmpqVta { get; set; }
        public string estatusAdmPorDivisa { get; set; }
        public decimal costoDivisaEmpqCompra { get; set; }
        public int contEmpqCompra { get; set; }
        public string estatusAplicaPorcAumento { get; set; }
    }
}