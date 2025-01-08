using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pedido.Entidad
{
    public class Item
    {
        public int id { get; set; }
        public string idProducto { get; set; }
        public string idDepartamento { get; set; }
        public string idGrupo { get; set; }
        public string idSubGrupo { get; set; }
        public string idTasaFiscal { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public decimal cantidadSolicita { get; set; }
        public decimal precioNetoMonAct { get; set; }
        public decimal precioFullMonDiv { get; set; }
        public string tarifaPrecio { get; set; }
        public decimal tasaIva { get; set; }
        public string tipoIva { get; set; }
        public string categoriaPrd { get; set; }
        public string decimalesPrd { get; set; }
        public string empaqueNombre { get; set; }
        public int empaqueCont { get; set; }
        public string estatusPesado { get; set; }
        public decimal costoEmqCompra { get; set; }
        public decimal costoEmqUnd { get; set; }
        public decimal costoPromedio { get; set; }
        public decimal costoPromedioUnd { get; set; }
        public string idDeposito { get; set; }
        public decimal peso { get; set; }
        public decimal volumen { get; set; }

    }
}