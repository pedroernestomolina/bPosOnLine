using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    public interface ICuadreCierre
    {
        DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.MetodoPago>
           CuadreCierre_Get_CuadreResumenMetodoPago_byId(int idResumen);
        DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.Documento>
           CuadreCierre_Get_CuadreResumenDocumento_byId(int idResumen);
        DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.CuadreResumen.Totales>
           CuadreCierre_Get_CuadreResumenTotalesd_byId(int idResumen);
        DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.PagoDetalle.Ficha>
           CuadreCierre_Reporte_PagoDetalle(int idResumen);
    }
}