using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.DocumentoAdm.Agregar.Pedido
{
    
    public class FichaEncabezado: BaseFicha
    {

        public FichaEncabezado()
        {
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
            RetencionIva = 0.0m;
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
            SubTotal = 0.0m;
            TipoCliente = "";
            Planilla = "";
            Expendiente = "";
            AnticipoIva = 0.0m;
            TercerosIva = 0.0m;
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
            EstatusValidado = "";
            Cierre = "";
            EstatusCierreContable = "";
            CierreFtp = "";
        }

    }

}