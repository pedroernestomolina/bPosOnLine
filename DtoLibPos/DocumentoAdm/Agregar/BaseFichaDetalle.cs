using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.DocumentoAdm.Agregar
{

    public abstract class BaseFichaDetalle
    {

        public string AutoProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre  { get; set; }
        public string AutoDepartamento { get; set; }
        public string AutoGrupo { get; set; }
        public string AutoSubGrupo { get; set; }
        public string AutoDeposito { get; set; }
        public decimal Cantidad { get; set; }
        public string Empaque { get; set; }
        public decimal PrecioNeto { get; set; }
        public decimal Descuento1p { get; set; }
        public decimal Descuento2p { get; set; }
        public decimal Descuento3p { get; set; }
        public decimal Descuento1 { get; set; }
        public decimal Descuento2 { get; set; }
        public decimal Descuento3 { get; set; }
        public decimal CostoVenta { get; set; }
        public decimal TotalNeto { get; set; }
        public decimal Tasa { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string EstatusAnulado { get; set; }
        public string Tipo { get; set; }
        public string Deposito { get; set; }
        public int Signo { get; set; }
        public decimal PrecioFinal { get; set; }
        public string AutoCliente { get; set; }
        public string Decimales { get; set; }
        public int ContenidoEmpaque { get; set; }
        public decimal CantidadUnd { get; set; }
        public decimal PrecioUnd { get; set; }
        public decimal CostoUnd { get; set; }
        public decimal Utilidad { get; set; }
        public decimal Utilidadp { get; set; }
        public decimal PrecioItem { get; set; }
        public string EstatusGarantia { get; set; }
        public string EstatusSerial { get; set; }
        public string CodigoDeposito { get; set; }
        public int DiasGarantia { get; set; }
        public string Detalle { get; set; }
        public decimal PrecioSugerido { get; set; }
        public string AutoTasa { get; set; }
        public string EstatusCorte { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }
        public string Corte  { get; set; }
        public string Categoria { get; set; }
        public decimal Cobranzap { get; set; }
        public decimal Ventasp { get; set; }
        public decimal CobranzapVendedor { get; set; }
        public decimal VentaspVendedor { get; set; }
        public decimal Cobranza { get; set; }
        public decimal Ventas { get; set; }
        public decimal CobranzaVendedor { get; set; }
        public decimal VentasVendedor { get; set; }
        public decimal CostoPromedioUnd { get; set; }
        public decimal CostoCompra { get; set; }
        public string EstatusChecked { get; set; }
        public string Tarifa { get; set; }
        public decimal TotalDescuento { get; set; }
        public string CodigoVendedor { get; set; }
        public string AutoVendedor { get; set; }
        public string CierreFtp { get; set; }

    }

}