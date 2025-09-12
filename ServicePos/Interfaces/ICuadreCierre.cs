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
           get_CuadreResumenMetodoPago_byId(int idResumen);
        DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.Documento>
           get_CuadreResumenDocumento_byId(int idResumen);
        DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.CuadreResumen.Totales>
           get_CuadreResumenTotalesd_byId(int idResumen);
    }
}