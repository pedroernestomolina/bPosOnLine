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
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.MetodoPago>
            CuadreCierre_Get_CuadreResumenMetodoPago_byId(int idResumen)
        {
            return ServiceProv.CuadreCierre_Get_CuadreResumenMetodoPago_byId(idResumen);
        }
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.Documento>
            CuadreCierre_Get_CuadreResumenDocumento_byId(int idResumen)
        {
            return ServiceProv.CuadreCierre_Get_CuadreResumenDocumento_byId(idResumen);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.CuadreResumen.Totales>
            CuadreCierre_Get_CuadreResumenTotalesd_byId(int idResumen)
        {
            return ServiceProv.CuadreCierre_Get_CuadreResumenTotalesd_byId(idResumen);
        }
        //
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.PagoDetalle.Ficha> 
            CuadreCierre_Reporte_PagoDetalle(int idResumen)
        {
            return ServiceProv.CuadreCierre_Reporte_PagoDetalle(idResumen);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.Reportes.PagoResumen.Ficha> 
            CuadreCierre_Reporte_PagoResumen(int idResumen)
        {
            return ServiceProv.CuadreCierre_Reporte_PagoResumen(idResumen);
        }
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.VentaCredito.Ficha> 
            CuadreCierre_Reporte_VentaCredito(int idResumen)
        {
            return ServiceProv.CuadreCierre_Reporte_VentaCredito(idResumen);
        }
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.CambiosVuelto.Ficha> 
            CuadreCierre_Reporte_CambiosVueltoEntregado(int idResumen)
        {
            return ServiceProv.CuadreCierre_Reporte_CambiosVueltoEntregado(idResumen);
        }
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.PagoMovil.Ficha> 
            CuadreCierre_Reporte_PagoMovil(int idResumen)
        {
            return ServiceProv.CuadreCierre_Reporte_PagoMovil(idResumen);
        }
        //
        public DtoLib.ResultadoEntidad<int> 
            CuadreCierre_CerrarPos(DtoLibPos.CuadreCierre.CerrarPos.Ficha ficha)
        {
            return ServiceProv.CuadreCierre_CerrarPos(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.ObtenerCierre.Ficha> 
            CuadreCierre_Get_ObtenerCierre_byIdOperador(int id)
        {
            return ServiceProv.CuadreCierre_Get_ObtenerCierre_byIdOperador(id);
        }
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.ObtenerCierre.Ficha> 
            CuadreCierre_Get_ListaCierre()
        {
            return ServiceProv.CuadreCierre_Get_ListaCierre();
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.ObtenerCierre.DataResumen.Ficha> 
            CuadreCierre_Get_ObtenerCierreDataResumen_byIdResumen(int idResumen)
        {
            return ServiceProv.CuadreCierre_Get_ObtenerCierreDataResumen_byIdResumen(idResumen);
        }
    }
}