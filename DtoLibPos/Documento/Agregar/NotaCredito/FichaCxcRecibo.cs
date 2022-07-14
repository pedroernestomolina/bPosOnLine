using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.NotaCredito
{
    
    public class FichaCxCRecibo: BaseCxCRecibo
    {


        public FichaCxCRecibo()
        {
            AutoUsuario = "";
            Importe = 0.0m;
            Usuario = "";
            MontoRecibido = 0.0m;
            Cobrador = "";
            AutoCliente = "";
            Cliente = "";
            CiRif = "";
            Codigo = "";
            EstatusAnulado = "";
            Direccion = "";
            Telefono = "";
            AutoCobrador = "";
            Anticipos = 0.0m;
            Cambio = 0.0m;
            Nota = "";
            CodigoCobrador = "";
            Retenciones = 0.0m;
            Descuentos = 0.0m;
            Cierre = "";
            CierreFtp = "";
            //
            ImporteDivisa = 0m;
            MontoRecibidoDivisa = 0m;
            CambioDivisa = 0m;
            CodigoSucursal = "";
        }

    }

}