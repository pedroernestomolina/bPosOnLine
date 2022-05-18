using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IReportesAdm
    {

        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Ficha> 
            ReportesAdm_GeneralDocumento(DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralPorDepartamento.Ficha> 
            ReportesAdm_GeneralPorDepartamento(DtoLibPos.Reportes.VentaAdministrativa.GeneralPorDepartamento.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralPorGrupo.Ficha> 
            ReportesAdm_GeneralPorGrupo(DtoLibPos.Reportes.VentaAdministrativa.GeneralPorGrupo.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Resumen.Ficha> 
            ReportesAdm_Resumen(DtoLibPos.Reportes.VentaAdministrativa.Resumen.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.VentaPorProducto.Ficha> 
            ReportesAdm_VentaPorProducto(DtoLibPos.Reportes.VentaAdministrativa.VentaPorProducto.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle.Ficha> 
            ReportesAdm_GeneralDocumentoDetalle(DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Consolidado.Ficha> 
            ReportesAdm_Consolidado(DtoLibPos.Reportes.VentaAdministrativa.Consolidado.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Consolidado.Ficha>
            ReportesAdm_UtilidadConsolidado(DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Venta.Ficha> 
            ReportesAdm_UtilidadVenta(DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Producto.Ficha> 
            ReportesAdm_UtilidadProducto(DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Ficha> 
            ReportesAdm_LibroVenta(DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Filtro filtro);

    }

}