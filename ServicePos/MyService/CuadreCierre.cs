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
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.PagoDetalle.Ficha> 
            CuadreCierre_Reporte_PagoDetalle(int idResumen)
        {
            return ServiceProv.CuadreCierre_Reporte_PagoDetalle(idResumen);
        }
    }
}