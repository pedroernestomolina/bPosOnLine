using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.DocumentoAdm.Entidad
{
    
    public class Ficha
    {

        public string Auto { get; set; }
        public string AutoDocCxC { get; set; }
        public string AutoReciboCxC { get; set; }
        public string AutoCliente { get; set; }
        public string AutoVendedor { get; set; }
        public string AutoTransporte { get; set; }
        public string AutoUsuario { get; set; }
        public string AutoRemision { get; set; }

        public string DocNumero { get; set; }
        public DateTime DocFecha { get; set; }
        public DateTime DocFechaVencimiento { get; set; }
        public int DocSigno { get; set; }
        public string DocCodigo { get; set; }
        public string DocTipo { get; set; }
        public string DocNombre { get; set; }

        public string ClienteRazonSocial { get; set; }
        public string ClienteDirFiscal { get; set; }
        public string ClienteCiRif { get; set; }
        public string ClienteCodigo { get; set; }
        public string ClienteTelefono { get; set; }

        public string VendedorCodigo { get; set; }
        public string VendedorNombre { get; set; }

        public string TransporteNombre { get; set; }
        public string TransporteCodigo { get; set; }

        public string UsuarioNombre { get; set; }
        public string UsuarioCodigo { get; set; }

        public string RemisionCodigoDoc { get; set; }
        public string RemisionNumeroDoc { get; set; }
        
        public decimal MontoDescuento1 { get; set; }
        public decimal MontoDescuento2 { get; set; }
        public decimal PorctDescuento1 { get; set; }
        public decimal PorctDescuento2 { get; set; }
        public decimal MontoCargos { get; set; }
        public decimal PorctCargos { get; set; }
        public decimal MontoExento { get; set; }
        public decimal MontoBase { get; set; }
        public decimal MontoImpuesto { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDivisa { get; set; }
        public decimal MontoBase1 { get; set; }
        public decimal MontoBase2 { get; set; }
        public decimal MontoBase3 { get; set; }
        public decimal MontoImpuesto1 { get; set; }
        public decimal MontoImpuesto2 { get; set; }
        public decimal MontoImpuesto3 { get; set; }
        public decimal Tasa1 { get; set; }
        public decimal Tasa2 { get; set; }
        public decimal Tasa3 { get; set; }

        public decimal FactorCambio { get; set; }
        public decimal SubTotalNeto { get; set; }
        public decimal SubTotalImpuesto { get; set; }
        public decimal SubTotal { get; set; }
        public decimal VentaNeto { get; set; }
        public decimal CostoNeto { get; set; }
        public decimal Utilidad { get; set; }
        public decimal PorctUtilidad { get; set; }
        public decimal Cambio { get; set; }

        public decimal TasaRetencionIva { get; set; }
        public decimal TasaRetencionIslr { get; set; }
        public decimal RetencionIva { get; set; }
        public decimal RetencionIslr { get; set; }
        public string RetencionComprobanteIva { get; set; }
        public string RetencionComprobanteIslr { get; set; }

        public string EstatusAnulado { get; set; }
        public string Aplica { get; set; }

        public string OrdenCompraNumero { get; set; }
        public string PedidoNumero { get; set; }
        public DateTime? PedidoFecha { get; set; }

        public string SucursalCodigo { get; set; }
        public string Estacion { get; set; }

        public string DocDirDespacho { get; set; }
        public string DocAnoRelacion { get; set; }
        public string DocMesRelacion { get; set; }
        public string DocNota { get; set; }
        public string DocHora { get; set; }
        public string DocCondicionPago { get; set; }
        public int DocDiasCredito { get; set; }
        public string DocNumControl { get; set; }
        public int DocRenglones { get; set; }
        public string DocTarifa { get; set; }
        public string DocSerie { get; set; }
        public int DocDiasValidez { get; set; }
        public string DocSituacion { get; set; }
        public decimal DocSaldoPendiente { get; set; }
        public List<Item> items;


        public Ficha()
        {
            Auto="";
            AutoDocCxC ="";
            AutoReciboCxC ="";
            AutoCliente ="";
            AutoVendedor ="";
            AutoTransporte ="";
            AutoUsuario ="";
            AutoRemision ="";

            DocNumero ="";
            DocFecha = DateTime.Now.Date;
            DocFechaVencimiento = DateTime.Now.Date;
            DocSigno =1;
            DocCodigo ="";
            DocTipo ="";
            DocNombre ="";

            ClienteRazonSocial="";
            ClienteDirFiscal ="";
            ClienteCiRif ="";
            ClienteCodigo ="";
            ClienteTelefono="";

            VendedorCodigo ="";
            VendedorNombre ="";

            TransporteNombre ="";
            TransporteCodigo ="";

            UsuarioNombre ="";
            UsuarioCodigo ="";

            RemisionCodigoDoc ="";
            RemisionNumeroDoc ="";

            MontoDescuento1 =0m;
            MontoDescuento2 =0m;
            PorctDescuento1 =0m;
            PorctDescuento2 =0m;
            MontoCargos =0m;
            PorctCargos=0m;
            MontoExento=0m; 
            MontoBase =0m;
            MontoImpuesto =0m;
            Total =0m;
            TotalDivisa =0m;
            MontoBase1 =0m;
            MontoBase2 =0m;
            MontoBase3 =0m;
            MontoImpuesto1 =0m;
            MontoImpuesto2 =0m;
            MontoImpuesto3 =0m;
            Tasa1 =0m;
            Tasa2=0m;
            Tasa3=0m;

            FactorCambio =0m;
            SubTotalNeto =0m;
            SubTotalImpuesto =0m;
            SubTotal =0m;
            VentaNeto =0m;
            CostoNeto =0m;
            Utilidad =0m;
            PorctUtilidad=0m;
            Cambio =0m;

            TasaRetencionIva =0m;
            TasaRetencionIslr =0m;
            RetencionIva =0m;
            RetencionIslr =0m;
            RetencionComprobanteIva ="";
            RetencionComprobanteIslr ="";

            EstatusAnulado=""; 
            Aplica="";

            OrdenCompraNumero ="";
            PedidoNumero ="";
            PedidoFecha =null;

            SucursalCodigo ="";
            Estacion ="";

            DocDirDespacho ="";
            DocAnoRelacion ="";
            DocMesRelacion ="";
            DocNota ="";
            DocHora ="";
            DocCondicionPago ="";
            DocDiasCredito =0;
            DocNumControl ="";
            DocRenglones =0;
            DocTarifa ="";
            DocSerie ="";
            DocDiasValidez =0;
            DocSituacion ="";
            DocSaldoPendiente =0m;

            items = new List<Item>();
        }

    }

}