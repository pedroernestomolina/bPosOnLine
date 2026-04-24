using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibPos.PedidoWeb
{
    public class CapturarItemTrasladarPisoVentaDto
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
        public decimal costoDivisa {get;set;}
        public decimal contEmpqCompra { get; set; }
    }

    public class CapturarEncTrasladarPisoVentaDto 
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NombreEntidad { get; set; }
        public string CiRifEntidad { get; set; }
        public string DirEntidad { get; set; }
        public string TelefonoEntidad { get; set; }
        public string IdSucursal { get; set; }
        public string IdDeposito { get; set; }
        public string DescSucursal { get; set; }
        public string DescDeposito { get; set; }
        public int IdWebCliente { get; set; }
        public decimal ImporteMonRef { get; set; }
        public decimal ImporteMonLocal { get; set; }
        public decimal TasaCambio { get; set; }
        public decimal TasaSistema { get; set; }
        public int CntArticulos { get; set; }
        public int CntItems { get; set; }
        public int PedidoNro { get; set; }
        public string EstatusAnulado { get; set; }
        public string EstatusProcesado { get; set; }
    }

    public class CapturarTrasladoPisoVentaDto
    {
        public CapturarEncTrasladarPisoVentaDto Datos { get; set; }
        public List<CapturarItemTrasladarPisoVentaDto> Items { get; set; }
    }
}