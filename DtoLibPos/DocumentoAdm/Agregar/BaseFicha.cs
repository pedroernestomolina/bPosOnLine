using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.DocumentoAdm.Agregar
{
    
    public abstract class  BaseFicha
    {

        public string RazonSocial { get; set; }
        public string DirFiscal { get; set; }
        public string CiRif { get; set; }
        public string Tipo { get; set; }
        public decimal Exento  { get; set; }
        public decimal Base1 { get; set; }
        public decimal Base2 { get; set; }
        public decimal Base3 { get; set; }
        public decimal Impuesto1 { get; set; }
        public decimal Impuesto2 { get; set; }
        public decimal Impuesto3 { get; set; }
        public decimal MBase { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public decimal Tasa1 { get; set; }
        public decimal Tasa2 { get; set; }
        public decimal Tasa3 { get; set; }
        public string Nota { get; set; }
        public decimal TasaRetencionIva  { get; set; }
        public decimal TasaRetencionIslr { get; set; }
        public decimal RetencionIva { get; set; }
        public decimal RetencionIslr { get; set; }
        public string AutoCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string Control { get; set; }
        public string OrdenCompra { get; set; }
        public int Dias { get; set; }
        public decimal Descuento1 { get; set; }
        public decimal Descuento2 { get; set; }
        public decimal Cargos { get; set; }
        public decimal Descuento1p { get; set; }
        public decimal Descuento2p { get; set; }
        public decimal Cargosp { get; set; }
        public string Columna { get; set; }
        public string EstatusAnulado { get; set; }
        public string Aplica { get; set; }
        public string ComprobanteRetencion { get; set; }
        public decimal SubTotalNeto { get; set; }
        public string Telefono { get; set; }
        public decimal FactorCambio { get; set; }
        public string CodigoVendedor { get; set; }
        public string Vendedor { get; set; }
        public string AutoVendedor { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Pedido { get; set; }
        public string CondicionPago { get; set; }
        public string Usuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string CodigoSucursal { get; set; }
        public string Prefijo { get; set; }
        public string Hora { get; set; }
        public string Transporte { get; set; }
        public string CodigoTransporte { get; set; }
        public decimal MontoDivisa { get; set; }
        public string Despachado { get; set; }
        public string DirDespacho { get; set; }
        public string Estacion { get; set; }
        public int Renglones { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string ComprobanteRetencionIslr { get; set; }
        public int  DiasValidez { get; set; }
        public string AutoUsuario { get; set; }
        public string AutoTransporte { get; set; }
        public string Situacion { get; set; }
        public int  Signo { get; set; }
        public string Serie { get; set; }
        public string Tarifa { get; set; }
        public string TipoRemision { get; set; }
        public string DocumentoRemision { get; set; }
        public string AutoRemision { get; set; }
        public string DocumentoNombre { get; set; }
        public decimal SubTotalImpuesto { get; set; }
        public decimal SubTotal { get; set; }
        public string TipoCliente { get; set; }
        public string Planilla { get; set; }
        public string Expendiente { get; set; }
        public decimal AnticipoIva { get; set; }
        public decimal TercerosIva { get; set; }
        public decimal Neto { get; set; }
        public decimal Costo { get; set; }
        public decimal Utilidad { get; set; }
        public decimal Utilidadp { get; set; }
        public string DocumentoTipo { get; set; }
        public string CiTitular { get; set; }
        public string NombreTitular { get; set; }
        public string CiBeneficiario { get; set; }
        public string NombreBeneficiario { get; set; }
        public string Clave { get; set; }
        public string DenominacionFiscal { get; set; }
        public decimal Cambio { get; set; }
        public string EstatusValidado { get; set; }
        public string Cierre { get; set; }
        public string EstatusCierreContable { get; set; }
        public string CierreFtp { get; set; }

    }

}