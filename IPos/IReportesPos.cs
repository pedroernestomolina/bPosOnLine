using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    public interface IReportesPos
    {
        DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoDetalle.Ficha> 
            ReportePos_PagoDetalle(DtoLibPos.Reportes.POS.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.PagoResumen.Ficha>
            ReportePos_PagoResumen(DtoLibPos.Reportes.POS.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoMovil.Ficha>
            ReportePos_PagoMovil(DtoLibPos.Reportes.POS.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.PosVerificador.DocVerificados.Ficha>
            ReportePosVerificados_DocVerificados(DtoLibPos.Reportes.PosVerificador.Filtro filtro);
        DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.VueltosEntregados.Ficha>
            ReportePos_VueltosEntregados(DtoLibPos.Reportes.POS.Filtro filtro);
        //
        DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.MovCaja.Ficha>
            ReportePos_MovCaja(DtoLibPos.Reportes.POS.MovCaja.Filtro filtro);
    }
}