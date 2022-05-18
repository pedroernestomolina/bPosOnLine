using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.NotaCredito
{

    public class FichaDetalle
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


        public FichaDetalle()
        {
            AutoProducto = "";
            Codigo = "";
            Nombre = "";
            AutoDepartamento = "";
            AutoGrupo = "";
            AutoSubGrupo = "";
            AutoDeposito = "";
            Cantidad = 0.0m;
            Empaque = "";
            PrecioNeto = 0.0m;
            Descuento1p = 0.0m;
            Descuento2p = 0.0m;
            Descuento3p = 0.0m;
            Descuento1 = 0.0m;
            Descuento2 = 0.0m;
            Descuento3 = 0.0m;
            CostoVenta = 0.0m;
            TotalNeto = 0.0m;
            Tasa = 0.0m;
            Impuesto = 0.0m;
            Total = 0.0m;
            EstatusAnulado = "";
            Tipo = "";
            Deposito = "";
            Signo = 1;
            PrecioFinal = 0.0m;
            AutoCliente = "";
            Decimales = "";
            ContenidoEmpaque = 0;
            CantidadUnd = 0.0m;
            PrecioUnd = 0.0m;
            CostoUnd = 0.0m;
            Utilidad = 0.0m;
            Utilidadp = 0.0m;
            PrecioItem = 0.0m;
            EstatusGarantia = "";
            EstatusSerial = "";
            CodigoDeposito = "";
            DiasGarantia = 0;
            Detalle = "";
            PrecioSugerido = 0.0m;
            AutoTasa = "";
            EstatusCorte = "";
            X = 0.0m;
            Y = 0.0m;
            Z = 0.0m;
            Corte = "";
            Categoria = "";
            Cobranzap = 0.0m;
            Ventasp = 0.0m;
            CobranzapVendedor = 0.0m;
            VentaspVendedor = 0.0m;
            Cobranza = 0.0m;
            Ventas = 0.0m;
            CobranzaVendedor = 0.0m;
            VentasVendedor = 0.0m;
            CostoPromedioUnd = 0.0m;
            CostoCompra = 0.0m;
            EstatusChecked = "";
            Tarifa = "";
            TotalDescuento = 0.0m;
            CodigoVendedor = "";
            AutoVendedor = "";
            CierreFtp = "";
        }

    }

}