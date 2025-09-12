using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.Factura
{
    public class Ficha
    {
        public int idOperador { get; set; }
        public string DocumentoNro { get; set; }
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

        public List<FichaDetalle> Detalles { get; set; }
        public List<FichaKardex> MovKardex { get; set; }
        public List<FichaDeposito> ActDeposito { get; set; }
        public FichaCxC DocCxC { get; set; }
        public FichaCxCPago DocCxCPago { get; set; }
        public FichaPosResumen Resumen { get; set; }
        public List<FichaPosVenta> PosVenta{ get; set; }
        public FichaSerie SerieFiscal { get; set; }
        public FichaPagoMovil PagoMovil { get; set; }
        public FichaClienteSaldo ClienteSaldo { get; set; }
        public List<FichaMedida> Medidas { get; set; }
        public List<FichaPrecio> Precios { get; set; }
        //
        public decimal PorctBonoPorPagoDivisa { get; set; }
        public decimal CantDivisaAplicaBonoPorPagoDivisa { get; set; }
        public decimal MontoBonoPorPagoDivisa { get; set; }
        public decimal MontoBonoEnDivisaPorPagoDivisa { get; set; }
        public decimal MontoPorVueltoEnEfectivo { get; set; }
        public decimal MontoPorVueltoEnDivisa { get; set; }
        public decimal MontoPorVueltoEnPagoMovil { get; set; }
        public int CantDivisaPorVueltoEnDivisa { get; set; }
        public string estatusPorBonoPorPagoDivisa { get; set; }
        public string estatusPorVueltoEnPagoMovil { get; set; }
        //
        public string estatusFiscal { get; set; }
        public int zFiscal { get; set; }
        //
        public string aplicaIGTF {get;set;}
        public decimal tasaIGTF {get;set;}
        public decimal baseAplicaIGTFMonAct {get;set;}
        public decimal baseAplicaIGTFMonDiv {get;set;}
        public decimal montoIGTF { get; set; }
        //
        public string estatusMostrarLibroVenta { get; set; }
        //
        public string estatusCredito { get; set; }
        //


        //
        public List<FichaPosResumenDetalleFormaPago> detalleFormaPago { get; set; }
        public  FichaPosResumenGeneral resumenGeneral { get; set; }
        //


        public Ficha()
        {
            idOperador = -1;
            DocumentoNro = "";
            RazonSocial = "";
            DirFiscal = "";
            CiRif = "";
            Tipo = "";
            Exento = 0.0m;
            Base1 = 0.0m;
            Base2 = 0.0m;
            Base3 = 0.0m;
            Impuesto1 = 0.0m;
            Impuesto2 = 0.0m;
            Impuesto3 = 0.0m;
            MBase = 0.0m;
            Impuesto = 0.0m;
            Total = 0.0m;
            Tasa1 = 0.0m;
            Tasa2 = 0.0m;
            Tasa3 = 0.0m;
            Nota = "";
            TasaRetencionIva = 0.0m;
            TasaRetencionIslr = 0.0m;
            RetencionIva=0.0m;
            RetencionIslr = 0.0m;
            AutoCliente = "";
            CodigoCliente = "";
            Control = "";
            OrdenCompra = "";
            Dias = 0;
            Descuento1 = 0.0m;
            Descuento2 = 0.0m;
            Cargos = 0.0m;
            Descuento1p = 0.0m;
            Descuento2p = 0.0m;
            Cargosp = 0.0m;
            Columna = "";
            EstatusAnulado = "";
            Aplica = "";
            ComprobanteRetencion = "";
            SubTotalNeto = 0.0m;
            Telefono = "";
            FactorCambio = 0.0m;
            CodigoVendedor = "";
            Vendedor = "";
            AutoVendedor = "";
            FechaPedido = DateTime.Now.Date;
            Pedido = "";
            CondicionPago = "";
            Usuario = "";
            CodigoUsuario = "";
            CodigoSucursal = "";
            Hora = "";
            Transporte = "";
            CodigoTransporte = "";
            MontoDivisa = 0.0m;
            Despachado = "";
            DirDespacho = "";
            Estacion = "";
            Renglones = 0;
            SaldoPendiente = 0.0m;
            ComprobanteRetencionIslr = "";
            DiasValidez = 0;
            AutoUsuario = "";
            AutoTransporte = "";
            Situacion = "";
            Signo = 1;
            Serie = "";
            Tarifa = "";
            TipoRemision = "";
            DocumentoRemision = "";
            AutoRemision = "";
            DocumentoNombre = "";
            SubTotalImpuesto = 0.0m;
            SubTotal=0.0m;
            TipoCliente = "";
            Planilla = "";
            Expendiente = "";
            AnticipoIva = 0.0m;
            TercerosIva=0.0m;
            Neto = 0.0m;
            Costo = 0.0m;
            Utilidad = 0.0m;
            Utilidadp = 0.0m;
            DocumentoTipo = "";
            CiTitular = "";
            NombreTitular = "";
            CiBeneficiario = "";
            NombreBeneficiario = "";
            Clave = "";
            DenominacionFiscal = "";
            Cambio = 0.0m;
            EstatusValidado="";
            Cierre="";
            EstatusCierreContable="";
            CierreFtp="";

            DocCxC = new  FichaCxC();
            DocCxCPago = new FichaCxCPago();
            Detalles = new List<FichaDetalle>();
            MovKardex = new List<FichaKardex>();
            ActDeposito = new List<FichaDeposito>();
            Resumen = new FichaPosResumen();
            PosVenta = new List<FichaPosVenta>();
            PagoMovil = null;
            SerieFiscal = null;
            ClienteSaldo = null;
            Medidas = new List<FichaMedida>();
            Precios = new List<FichaPrecio>();
            //
            PorctBonoPorPagoDivisa = 0m;
            CantDivisaAplicaBonoPorPagoDivisa = 0m;
            MontoBonoPorPagoDivisa = 0m;
            MontoBonoEnDivisaPorPagoDivisa = 0m;
            MontoPorVueltoEnEfectivo = 0m;
            MontoPorVueltoEnDivisa = 0m;
            MontoPorVueltoEnPagoMovil = 0m;
            CantDivisaPorVueltoEnDivisa = 0;
            estatusPorBonoPorPagoDivisa = "0";
            estatusPorVueltoEnPagoMovil = "0";
            //
            estatusFiscal = "";
            zFiscal = -1;
            //
            aplicaIGTF="";
            tasaIGTF=0.0m;
            baseAplicaIGTFMonAct=0.0m;
            baseAplicaIGTFMonDiv=0.0m;
            montoIGTF=0.0m;
            //
            estatusMostrarLibroVenta = "";
            //
            estatusCredito = "";

            //
            detalleFormaPago = new List<FichaPosResumenDetalleFormaPago>();
            resumenGeneral = new FichaPosResumenGeneral();
        }
    }
}