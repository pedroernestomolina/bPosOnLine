using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibPos.PedidoWeb
{
    public enum EstatusProceso {SinDefinir=-1, SinProcesar=0, Procesado};

    public class FiltroPedidoWebListaRequest
    {
        public bool? FiltrarSoloActivo  { get; set; }
        public EstatusProceso FiltrarPorEstatusProceso { get; set; }
    }

    public class ItemsDepositoBloquearExRequest 
    {
        public string idProducto { get; set; }
        public string idDeposito { get; set; }
        public decimal cntBloquear { get; set; }
    }

    public class ItemsPisoVentaRequest
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
    }

    public class AplicarTrasladoPisoVentaRequest
    {
        public int IdOperador { get; set; }
        public string IdDeposito { get; set; }
        public List<ItemsDepositoBloquearExRequest> ItemBloqEx { get; set; }
        public List<ItemsPisoVentaRequest> ItemsPisoVta { get; set; }
        public AplicarTrasladoPisoVentaRequest()
        {
            IdOperador = -1;
            IdDeposito = "";
            ItemBloqEx = new List<ItemsDepositoBloquearExRequest>();
            ItemsPisoVta = new List<ItemsPisoVentaRequest>();
        }
    }
}