using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{
    public partial class Service : IService
    {
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_ModoPos()
        {
            return ServiceProv.Configuracion_ModoPos();
        }

        public DtoLib.ResultadoEntidad<string> 
            Configuracion_FactorDivisa()
        {
            return ServiceProv.Configuracion_FactorDivisa();
        }
        public DtoLib.Resultado 
            Configuracion_Pos_Actualizar(DtoLibPos.Configuracion.Actualizar.Ficha ficha)
        {
            return ServiceProv.Configuracion_Pos_Actualizar(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Entidad.Ficha> 
            Configuracion_Pos_GetFicha()
        {
            return ServiceProv.Configuracion_Pos_GetFicha();
        }
        public DtoLib.Resultado 
            Configuracion_Pos_Inicializar()
        {
            return ServiceProv.Configuracion_Pos_Inicializar();
        }
        public DtoLib.Resultado 
            Configuracion_Pos_CambioDepositoSucursalFrio()
        {
            return ServiceProv.Configuracion_Pos_CambioDepositoSucursalFrio();
        }
        public DtoLib.Resultado 
            Configuracion_Pos_CambioDepositoSucursalViveres()
        {
            return ServiceProv.Configuracion_Pos_CambioDepositoSucursalViveres();
        }
        public DtoLib.Resultado 
            Configuracion_Pos_CambioDepositoSucursal(DtoLibPos.Configuracion.CambioDepositoSucursal.Ficha ficha)
        {
            return ServiceProv.Configuracion_Pos_CambioDepositoSucursal(ficha);
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_Habilitar_Precio5_VentaMayor()
        {
            return ServiceProv.Configuracion_Habilitar_Precio5_VentaMayor();
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_ValorMaximoPorcentajeDescuento()
        {
            return ServiceProv.Configuracion_ValorMaximoPorcentajeDescuento();
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_HabilitarDescuentoUnicamenteConPagoEnDivsa()
        {
            return ServiceProv.Configuracion_HabilitarDescuentoUnicamenteConPagoEnDivsa();
        }
        //
        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Configuracion_IGTF> 
            Configuracion_IGTF()
        {
            return ServiceProv.Configuracion_IGTF();
        }
        //
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_TasaCambioSistema()
        {
            return ServiceProv.Configuracion_TasaCambioSistema();
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_PorcentajeAumentarEnPreciosDeProductosNoAdministradoPorDivisa()
        {
            return ServiceProv.Configuracion_PorcentajeAumentarEnPreciosDeProductosNoAdministradoPorDivisa();
        }

        //
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_MonedaLocal()
        {
            return ServiceProv.Configuracion_MonedaLocal();
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_MonedaReferencia()
        {
            return ServiceProv.Configuracion_MonedaReferencia();
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_MedioPagoPorPagoBonoDivisa()
        {
            return ServiceProv.Configuracion_MedioPagoPorPagoBonoDivisa(); 
        }
    }
}