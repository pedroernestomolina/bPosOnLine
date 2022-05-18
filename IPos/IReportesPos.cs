using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IReportesPos
    {

        DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoDetalle.Ficha> ReportePos_PagoDetalle(DtoLibPos.Reportes.POS.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.PagoResumen.Ficha> ReportePos_PagoResumen(DtoLibPos.Reportes.POS.Filtro filtro);

    }

}