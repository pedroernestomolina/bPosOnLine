using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.NotaCredito
{
    public class FichaCxCMetodoPago: BaseCxCMetodoPago
    {
        public decimal MontoMonedaRecibe { get; set; }
        public string CodigoMonedaRecibe { get; set; }
        public string SimboloMonedaRecibe { get; set; }
        public decimal TasaMonedaRecibe { get; set; }
        public string LoteNroMonedaRecibe { get; set; }
        public string ReferenciaNroMonedaRecibe { get; set; }
        public decimal MontoMonedaLocal { get; set; }
        public decimal MontoMonedaReferencia { get; set; }
        public FichaCxCMetodoPago()
        {
            AutoMedioPago = "";
            AutoAgencia = "";
            Medio = "";
            Codigo = "";
            MontoRecibido = 0.0m;
            EstatusAnulado = "";
            Numero = "";
            Agencia = "";
            AutoUsuario = "";
            Lote = "";
            Referencia = "";
            AutoCobrador = "";
            Cierre = "";
            CierreFtp = "";
            //
            OpBanco = "";
            OpNroCta = "";
            OpNroRef = "";
            OpFecha = DateTime.Now.Date;
            OpDetalle = "";
            OpMonto = 0m;
            OpTasa = 0m;
            OpAplicaConversion = "";
            CodigoSucursal = "";
            //
            //
            MontoMonedaRecibe = 0m;
            CodigoMonedaRecibe = "";
            SimboloMonedaRecibe = "";
            TasaMonedaRecibe = 0m;
            LoteNroMonedaRecibe = "";
            ReferenciaNroMonedaRecibe = "";
            MontoMonedaLocal = 0m;
            MontoMonedaReferencia = 0m;
        }
    }
}