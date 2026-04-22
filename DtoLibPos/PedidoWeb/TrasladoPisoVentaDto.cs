using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibPos.PedidoWeb
{
    public class ItemsTrasladarPisoVentaDto
    {
        public string idProducto { get; set; }
        public string idDepartamento { get; set; }
        public string idGrupo { get; set; }
        public string idSubGrupo { get; set; }
        public string idTasaFiscal { get; set; }
        public string codigoPrd { get; set; }
        public string nombrePrd { get; set; }
        public int cntSolicitada { get; set; }
        public decimal pNeto { get; set; }
        public decimal pDivisaFull { get; set; }
        public decimal tasaFiscal { get; set; }
        public string categoriaPrd { get; set; }
        public string decimalesPrd { get; set; }
        public string descEmpq { get; set; }
        public int contEmpq { get; set; }
        public string estatusPesado { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoPromUnd { get; set; }
        public decimal costoCompra { get; set; }
        public decimal costoProm { get; set; }
        public decimal pesoPrd { get; set; }
        public decimal volumenPrd { get; set; }
        public string estatusDivisa { get; set; }
        public decimal exDisponible { get; set; }
    }

    public class CapturarTrasladoPisoVentaDto
    {
        public List<ItemsTrasladarPisoVentaDto> Items { get; set; }
    }
}