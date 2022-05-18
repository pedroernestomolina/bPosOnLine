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

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Ficha> 
            ReportesAdm_GeneralDocumento(DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_GeneralDocumento(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralPorDepartamento.Ficha> 
            ReportesAdm_GeneralPorDepartamento(DtoLibPos.Reportes.VentaAdministrativa.GeneralPorDepartamento.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_GeneralPorDepartamento(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralPorGrupo.Ficha> 
            ReportesAdm_GeneralPorGrupo(DtoLibPos.Reportes.VentaAdministrativa.GeneralPorGrupo.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_GeneralPorGrupo(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Resumen.Ficha> 
            ReportesAdm_Resumen(DtoLibPos.Reportes.VentaAdministrativa.Resumen.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_Resumen(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.VentaPorProducto.Ficha> 
            ReportesAdm_VentaPorProducto(DtoLibPos.Reportes.VentaAdministrativa.VentaPorProducto.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_VentaPorProducto(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle.Ficha> 
            ReportesAdm_GeneralDocumentoDetalle(DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_GeneralDocumentoDetalle(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Consolidado.Ficha> 
            ReportesAdm_Consolidado(DtoLibPos.Reportes.VentaAdministrativa.Consolidado.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_Consolidado (filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Venta.Ficha> 
            ReportesAdm_Utilidad_Venta(DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_UtilidadVenta(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Producto.Ficha> 
            ReportesAdm_Utilidad_Producto(DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_UtilidadProducto(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Ficha> 
            ReportesAdm_LibroVenta(DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_LibroVenta(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Consolidado.Ficha> 
            ReportesAdm_Utilidad_Consolidado(DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro filtro)
        {
            return ServiceProv.ReportesAdm_UtilidadConsolidado(filtro);
        }

    }

}