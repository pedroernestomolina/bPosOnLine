using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Configuracion.Entidad
{
    
    public class Ficha
    {

        public string idSucursal { get; set; }
        public string idDeposito { get; set; }
        public string idVendedor { get; set; }
        public string idCobrador { get; set; }
        public string idTransporte { get; set; }
        public string idMedioPagoEfectivo { get; set; }
        public string idMedioPagoDivisa { get; set; }
        public string idMedioPagoElectronico { get; set; }
        public string idMedioPagoOtros { get; set; }
        public string idConceptoVenta { get; set; }
        public string idConceptoDevVenta { get; set; }
        public string idConceptoSalida { get; set; }
        public string idTipoDocVenta{ get; set; }
        public string idTipoDocDevVenta{ get; set; }
        public string idTipoDocNotaEntrega{ get; set; }
        public string idFacturaSerie { get; set; }
        public string idNotaCreditoSerie { get; set; }
        public string idNotaEntregaSerie { get; set; }
        public string idNotaDebitoSerie { get; set; }
        //
        public string idClaveUsar { get; set; }
        public string idPrecioManejar { get; set; }
        public string validarExistencia { get; set; }
        public string activarBusquedaPorDescripcion { get; set; }
        public string activarRepesaje { get; set; }
        public decimal limiteInferiorRepesaje { get; set; }
        public decimal limiteSuperiorRepesaje { get; set; }
        //
        public string modoPrecio { get; set; }
        public string estatus { get; set; }


        public Ficha()
        {
            idSucursal = "";
            idDeposito = "";
            idVendedor = "";
            idCobrador = "";
            idTransporte = "";
            idMedioPagoDivisa = "";
            idMedioPagoEfectivo = "";
            idMedioPagoElectronico = "";
            idMedioPagoOtros = "";
            idConceptoDevVenta = "";
            idConceptoSalida = "";
            idConceptoVenta = "";
            idTipoDocDevVenta = "";
            idTipoDocNotaEntrega = "";
            idTipoDocVenta = "";
            idFacturaSerie = "";
            idNotaCreditoSerie = "";
            idNotaEntregaSerie = "";
            idNotaDebitoSerie = "";
            //
            idClaveUsar = "";
            idPrecioManejar = "";
            validarExistencia = "";
            activarBusquedaPorDescripcion = "";
            activarRepesaje = "";
            limiteInferiorRepesaje = 0.0m;
            limiteSuperiorRepesaje = 0.0m;
            //
            modoPrecio = "";
            estatus = "";
        }

    }

}