using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.Factura
{
    
    public class FichaCxC: BaseCxC
    {


        public FichaCxC()
        {
            CCobranza = 0.0m;
            CCobranzap = 0.0m;
            TipoDocumento = "";
            Nota = "";
            Importe = 0.0m;
            Acumulado = 0.0m;
            AutoCliente="";
            Cliente = "";
            CiRif = "";
            CodigoCliente = "";
            EstatusCancelado = "";
            Resta = 0.0m;
            EstatusAnulado = "";
            Numero = "";
            AutoAgencia = "";
            Agencia = "";
            Signo = 1;
            AutoVendedor = "";
            CDepartamento = 0.0m;
            CVentas = 0.0m;
            CVentasp = 0.0m;
            Serie = "";
            ImporteNeto = 0.0m;
            Dias = 0;
            CastigoP = 0.0m;
            CierreFtp = "";
            MontoDivisa = 0m;
            TasaDivisa = 0m;
            //
            AcumuladoDivisa = 0m;
            CodigoSucursal = "";
            RestaDivisa = 0m;
            ImporteNetoDivisa = 0m;
        }

    }

}