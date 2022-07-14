using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.Factura
{

    public class FichaCxCMetodoPago: BaseCxCMetodoPago
    {


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
        }

    }

}